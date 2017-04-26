using Chip8Interpreter.Core.Types;
using System.Collections.Generic;
using System;

namespace Chip8Interpreter.Core
{
    public class CPU
    {
        private byte[] generalRegisters;
        private ushort addressRegister;
        private Timer.DelayTimer delayTimer;
        private Timer.SoundTimer soundTimer;
        private ushort programCounter;
        private Stack stack;
        private Memory memory;
        private Display display;
        private Keypad keypad;

        private delegate void Instruction(ushort opcode);
        private Dictionary<int, ushort> opcodeIDMaskLookup;
        private Dictionary<int, Instruction> opcodeLookup;
        private Dictionary<int, string> opcodeDescriptionLookup;

        private Random randomGenerator;

        public CPU(Memory memory, Display display, Keypad keypad)
        {
            generalRegisters = new byte[16];
            addressRegister = 0;
            delayTimer = new Timer.DelayTimer();
            soundTimer = new Timer.SoundTimer();
            programCounter = Memory.ProgramDataStart;
            stack = new Stack();
            this.memory = memory;
            this.display = display;
            this.keypad = keypad;
            InitializeOpcodeIDMaskLookup();
            InitializeOpcodeLookup();
            InitializeOpcodeDescriptionLookup();
            randomGenerator = new Random();
        }

        private void InitializeOpcodeIDMaskLookup()
        {
            // Key is first 4 most significant bits of opcode (not shifted).
            // Value is mask needed to mask all non-identifying bits of the opcode.
            opcodeIDMaskLookup = new Dictionary<int, ushort>();
            opcodeIDMaskLookup.Add(0x0000, 0xFFFF);
            opcodeIDMaskLookup.Add(0x1000, 0xF000);
            opcodeIDMaskLookup.Add(0x2000, 0xF000);
            opcodeIDMaskLookup.Add(0x3000, 0xF000);
            opcodeIDMaskLookup.Add(0x4000, 0xF000);
            opcodeIDMaskLookup.Add(0x5000, 0xF00F);
            opcodeIDMaskLookup.Add(0x6000, 0xF000);
            opcodeIDMaskLookup.Add(0x7000, 0xF000);
            opcodeIDMaskLookup.Add(0x8000, 0xF00F);
            opcodeIDMaskLookup.Add(0x9000, 0xF00F);
            opcodeIDMaskLookup.Add(0xA000, 0xF000);
            opcodeIDMaskLookup.Add(0xB000, 0xF000);
            opcodeIDMaskLookup.Add(0xC000, 0xF000);
            opcodeIDMaskLookup.Add(0xD000, 0xF000);
            opcodeIDMaskLookup.Add(0xE000, 0xF0FF);
            opcodeIDMaskLookup.Add(0xF000, 0xF0FF);
        }

        private void InitializeOpcodeLookup()
        {
            opcodeLookup = new Dictionary<int, Instruction>();
            opcodeLookup.Add(0x00E0, new Instruction(ClearScreen));
            opcodeLookup.Add(0x00EE, new Instruction(Return));
            opcodeLookup.Add(0x1000, new Instruction(Jump_Direct));
            opcodeLookup.Add(0x2000, new Instruction(Call_Direct));
            opcodeLookup.Add(0x3000, new Instruction(SkipIfEqual_Immediate));
            opcodeLookup.Add(0x4000, new Instruction(SkipIfNotEqual_Immediate));
            opcodeLookup.Add(0x5000, new Instruction(SkipIfEqual_Register));
            opcodeLookup.Add(0x6000, new Instruction(Load_Immediate));
            opcodeLookup.Add(0x7000, new Instruction(Add_Immediate));
            opcodeLookup.Add(0x8000, new Instruction(MoveRegister));
            opcodeLookup.Add(0x8001, new Instruction(Or_Register));
            opcodeLookup.Add(0x8002, new Instruction(And_Register));
            opcodeLookup.Add(0x8003, new Instruction(XOr_Register));
            opcodeLookup.Add(0x8004, new Instruction(Add_Register));
            opcodeLookup.Add(0x8005, new Instruction(Subtract_Register));
            opcodeLookup.Add(0x8006, new Instruction(ShiftRight));
            opcodeLookup.Add(0x8007, new Instruction(SubractReversed_Register));
            opcodeLookup.Add(0x800E, new Instruction(ShiftLeft));
            opcodeLookup.Add(0x9000, new Instruction(SkipIfNotEqual_Register));
            opcodeLookup.Add(0xA000, new Instruction(LoadAddress_Immediate));
            opcodeLookup.Add(0xB000, new Instruction(Jump_V0_Direct));
            opcodeLookup.Add(0xC000, new Instruction(RandomAnd_Immediate));
            opcodeLookup.Add(0xD000, new Instruction(DrawSprite));
            opcodeLookup.Add(0xE09E, new Instruction(SkipIfKeyPressed_Register));
            opcodeLookup.Add(0xE0A1, new Instruction(SkipIfKeyNotPressed_Register));
            opcodeLookup.Add(0xF007, new Instruction(LoadDelayTimer));
            opcodeLookup.Add(0xF00A, new Instruction(LoadNextKeyPress));
            opcodeLookup.Add(0xF015, new Instruction(SetDelayTimer_Register));
            opcodeLookup.Add(0xF018, new Instruction(SetSoundTimer_Register));
            opcodeLookup.Add(0xF01E, new Instruction(AdvanceAddress_Register));
            opcodeLookup.Add(0xF029, new Instruction(PointToCharacterSprite_Register));
            opcodeLookup.Add(0xF033, new Instruction(StoreDecimal));
            opcodeLookup.Add(0xF055, new Instruction(StoreRegistersThroughX));
            opcodeLookup.Add(0xF065, new Instruction(LoadRegistersThroughX));
        }

        private void InitializeOpcodeDescriptionLookup()
        {
            opcodeDescriptionLookup = new Dictionary<int, string>();
            opcodeDescriptionLookup.Add(0x00E0, "Clear Screen");
            opcodeDescriptionLookup.Add(0x00EE, "Return");
            opcodeDescriptionLookup.Add(0x1000, "Jump - Direct");
            opcodeDescriptionLookup.Add(0x2000, "Call - Direct");
            opcodeDescriptionLookup.Add(0x3000, "Skip if equal - immediate");
            opcodeDescriptionLookup.Add(0x4000, "Skip if not equal - immediate");
            opcodeDescriptionLookup.Add(0x5000, "Skip if equal - register");
            opcodeDescriptionLookup.Add(0x6000, "Load - Immediate");
            opcodeDescriptionLookup.Add(0x7000, "Add - Immediate");
            opcodeDescriptionLookup.Add(0x8000, "Move - Register");
            opcodeDescriptionLookup.Add(0x8001, "Or - Register");
            opcodeDescriptionLookup.Add(0x8002, "And - Register");
            opcodeDescriptionLookup.Add(0x8003, "XOr - Register");
            opcodeDescriptionLookup.Add(0x8004, "Add - Register");
            opcodeDescriptionLookup.Add(0x8005, "Subtract - Register");
            opcodeDescriptionLookup.Add(0x8006, "Shift Right");
            opcodeDescriptionLookup.Add(0x8007, "Subtract Reversed - Register");
            opcodeDescriptionLookup.Add(0x800E, "Shift Left");
            opcodeDescriptionLookup.Add(0x9000, "Skip if not equal - register");
            opcodeDescriptionLookup.Add(0xA000, "Load Address - Immediate");
            opcodeDescriptionLookup.Add(0xB000, "Jump Direct + V0 offset");
            opcodeDescriptionLookup.Add(0xC000, "Random And - Immediate");
            opcodeDescriptionLookup.Add(0xD000, "Draw Sprite");
            opcodeDescriptionLookup.Add(0xE09E, "Skip if key pressed");
            opcodeDescriptionLookup.Add(0xE0A1, "Skip if key not pressed");
            opcodeDescriptionLookup.Add(0xF007, "Load Delay Timer");
            opcodeDescriptionLookup.Add(0xF00A, "Load Next Key Press");
            opcodeDescriptionLookup.Add(0xF015, "Set Delay Timer - Register");
            opcodeDescriptionLookup.Add(0xF018, "Set Sound Timer - Register");
            opcodeDescriptionLookup.Add(0xF01E, "Advance Address - Register");
            opcodeDescriptionLookup.Add(0xF029, "Point To Character Sprite - Register");
            opcodeDescriptionLookup.Add(0xF033, "Store Decimal");
            opcodeDescriptionLookup.Add(0xF055, "Store Registers through X");
            opcodeDescriptionLookup.Add(0xF065, "Load Registers through X");
        }

        public void RunCycle()
        {
            // Fetch opcode.
            ushort opcode = (ushort)((memory.ReadByte(programCounter) << 8) | memory.ReadByte(programCounter + 1));
            programCounter += 2;
            // Mask out non-identifying bits to get a unique opcode identifying value.
            int opcodeID = opcode & opcodeIDMaskLookup[opcode & 0xF000];
            Instruction execute;
            if(opcodeLookup.TryGetValue(opcodeID, out execute))
            {
                execute(opcode);
            }
            else
            {
                // Handle ambiguous System Jump case.
                if((opcode & 0xF000) == 0)
                {
                    SystemJump(opcode);
                }
                else
                {
                    throw new Exception("Unimplemented instruction: " + opcode.ToString("X4"));
                }
            }
        }


        // Opcode functions.

        private void ClearScreen(ushort opcode)
        {
            display.ClearDisplay();
        }

        private void Return(ushort opcode)
        {
            programCounter = stack.Pop();
        }

        private void SystemJump(ushort opcode)
        {
            // Only relevent on original machines.
        }

        private void Jump_Direct(ushort opcode)
        {
            programCounter = (ushort)(opcode & 0x0FFF);
        }

        private void Call_Direct(ushort opcode)
        {
            stack.Push(programCounter);
            programCounter = (ushort)(opcode & 0x0FFF);
        }

        private void SkipIfEqual_Immediate(ushort opcode)
        {
            if(generalRegisters[(opcode & 0x0F00) >> 8] == (opcode & 0x00FF))
            {
                programCounter += 2;
            }
        }

        private void SkipIfNotEqual_Immediate(ushort opcode)
        {
            if(generalRegisters[((opcode & 0x0F00) >> 8)] != (opcode & 0x00FF))
            {
                programCounter += 2;
            }
        }

        private void SkipIfEqual_Register(ushort opcode)
        {
            if(generalRegisters[(opcode & 0x0F00) >> 8] == generalRegisters[(opcode & 0x00F0) >> 4])
            {
                programCounter += 2;
            }
        }

        private void Load_Immediate(ushort opcode)
        {
            generalRegisters[(opcode & 0x0F00) >> 8] = (byte)(opcode & 0x00FF);
        }

        private void Add_Immediate(ushort opcode)
        {
            generalRegisters[(opcode & 0x0F00) >> 8] += (byte)(opcode & 0x00FF);
        }

        private void MoveRegister(ushort opcode)
        {
            generalRegisters[(opcode & 0x0F00) >> 8] = generalRegisters[(opcode & 0x00F0) >> 4];
        }

        private void Or_Register(ushort opcode)
        {
            generalRegisters[(opcode & 0x0F00) >> 8] |= generalRegisters[(opcode & 0x00F0) >> 4];
        }

        private void And_Register(ushort opcode)
        {
            generalRegisters[(opcode & 0x0F00) >> 8] &= generalRegisters[(opcode & 0x00F0) >> 4];
        }

        private void XOr_Register(ushort opcode)
        {
            generalRegisters[(opcode & 0x0F00) >> 8] ^= generalRegisters[(opcode & 0x00F0) >> 4];
        }

        private void Add_Register(ushort opcode)
        {
            int registerIndex0 = (opcode & 0x0F00) >> 8;
            int registerIndex1 = (opcode & 0x00F0) >> 4;
            int sum = generalRegisters[registerIndex0] + generalRegisters[registerIndex1];
            if(sum > 255)
            {
                generalRegisters[15] = 1;
            }
            else
            {
                generalRegisters[15] = 0;
            }
            generalRegisters[registerIndex0] = (byte)sum;
        }

        private void Subtract_Register(ushort opcode)
        {
            int registerIndex0 = (opcode & 0x0F00) >> 8;
            int registerIndex1 = (opcode & 0x00F0) >> 4;
            int difference = generalRegisters[registerIndex0] - generalRegisters[registerIndex1];
            if(difference < 0)
            {
                generalRegisters[15] = 0;
            }
            else
            {
                generalRegisters[15] = 1;
            }
            generalRegisters[registerIndex0] = (byte)difference;
        }

        private void ShiftRight(ushort opcode)
        {
            int srcRegisterIndex = (opcode & 0x00F0) >> 4;
            int dstRegisterIndex = (opcode & 0x0F00) >> 8;
            // Move least significant bit of source register to VF.
            generalRegisters[15] = (byte)(generalRegisters[srcRegisterIndex] & 0x01);
            generalRegisters[dstRegisterIndex] = (byte)(generalRegisters[srcRegisterIndex] >> 1);
        }

        private void SubractReversed_Register(ushort opcode)
        {
            int registerIndex0 = (opcode & 0x0F00) >> 8;
            int registerIndex1 = (opcode & 0x00F0) >> 4;
            int difference = generalRegisters[registerIndex1] - generalRegisters[registerIndex0];
            if (difference < 0)
            {
                generalRegisters[15] = 0;
            }
            else
            {
                generalRegisters[15] = 1;
            }
            generalRegisters[registerIndex0] = (byte)difference;
        }

        private void ShiftLeft(ushort opcode)
        {
            int srcRegisterIndex = (opcode & 0x00F0) >> 4;
            int dstRegisterIndex = (opcode & 0x0F00) >> 8;
            // Move most significant bit of source register to VF.
            generalRegisters[15] = (byte)((generalRegisters[srcRegisterIndex] & 0x80) >> 7);
            generalRegisters[dstRegisterIndex] = (byte)(generalRegisters[srcRegisterIndex] << 1);
        }

        private void SkipIfNotEqual_Register(ushort opcode)
        {
            if(generalRegisters[(opcode & 0x0F00) >> 8] != generalRegisters[(opcode & 0x00F0) >> 4])
            {
                programCounter += 2;
            }
        }

        private void LoadAddress_Immediate(ushort opcode)
        {
            addressRegister = (ushort)(opcode & 0x0FFF);
        }

        private void Jump_V0_Direct(ushort opcode)
        {
            programCounter = (ushort)((opcode & 0x0FFF) + generalRegisters[0]);
        }

        private void RandomAnd_Immediate(ushort opcode)
        {
            byte random = (byte)randomGenerator.Next(0, 256);
            generalRegisters[(opcode & 0x0F00) >> 8] = (byte)(random & (opcode & 0x00FF));
        }

        private void DrawSprite(ushort opcode)
        {
            byte x = generalRegisters[(opcode & 0x0F00) >> 8];
            byte y = generalRegisters[(opcode & 0x00F0) >> 4];
            int spriteSize = opcode & 0x000F;
            byte[] spriteData = new byte[spriteSize];
            byte[] memoryData = memory.GetData();
            Array.Copy(memoryData, addressRegister, spriteData, 0, spriteSize);
            display.DrawSprite(x, y, spriteData);
        }

        private void SkipIfKeyPressed_Register(ushort opcode)
        {
            if(keypad.GetKeyState(generalRegisters[(opcode & 0x0F00) >> 8]))
            {
                programCounter += 2;
            }
        }

        private void SkipIfKeyNotPressed_Register(ushort opcode)
        {
            if (!keypad.GetKeyState(generalRegisters[(opcode & 0x0F00) >> 8]))
            {
                programCounter += 2;
            }
        }

        private void LoadDelayTimer(ushort opcode)
        {
            generalRegisters[(opcode & 0x0F00) >> 8] = delayTimer.GetValue();
        } 

        private void LoadNextKeyPress(ushort opcode)
        {
            generalRegisters[(opcode & 0x0F00) >> 8] = keypad.GetNextKeyPress();
        }

        private void SetDelayTimer_Register(ushort opcode)
        {
            delayTimer.SetValue(generalRegisters[(opcode & 0x0F00) >> 8]);
        }

        private void SetSoundTimer_Register(ushort opcode)
        {
            soundTimer.SetValue(generalRegisters[(opcode & 0x0F00) >> 8]);
        }

        private void AdvanceAddress_Register(ushort opcode)
        {
            addressRegister += generalRegisters[(opcode & 0x0F00) >> 8];
        }

        private void PointToCharacterSprite_Register(ushort opcode)
        {
            addressRegister = (ushort)(5 * (generalRegisters[(opcode & 0x0F00) >> 8] & 0x0F));
        }

        private void StoreDecimal(ushort opcode)
        {
            byte value = generalRegisters[(opcode & 0x0F00) >> 8];
            memory.WriteByte((byte)(value / 100), addressRegister);
            addressRegister++;
            memory.WriteByte((byte)((value % 100) / 10), addressRegister);
            addressRegister++;
            memory.WriteByte((byte)(value % 10), addressRegister);
            addressRegister++;
        }

        private void StoreRegistersThroughX(ushort opcode)
        {
            int x = (opcode & 0x0F00) >> 8;
            for (int i = 0; i <= x; i++)
            {
                memory.WriteByte(generalRegisters[i], addressRegister);
                addressRegister++;
            }
        }

        private void LoadRegistersThroughX(ushort opcode)
        {
            int x = (opcode & 0x0F00) >> 8;
            for (int i = 0; i <= x; i++)
            {
                generalRegisters[i] = memory.ReadByte(addressRegister);
                addressRegister++;
            }
        }



        public string GetInstructionDescription(ushort opcode)
        {
            int opcodeID = opcode & opcodeIDMaskLookup[opcode & 0xF000];
            string description;
            if(!opcodeDescriptionLookup.TryGetValue(opcodeID, out description))
            {
                if((opcode & 0xF000) == 0)
                {
                    description = "System Jump";
                }
                else
                {
                    description = "No Description";
                }
            }
            return description;
        }


        // Accessors.

        public byte[] GetGeneralRegisters()
        {
            return generalRegisters;
        }

        public ushort GetAddressRegister()
        {
            return addressRegister;
        }

        public ushort GetProgramCounter()
        {
            return programCounter;
        }

        public Timer.DelayTimer GetDelayTimer()
        {
            return delayTimer;
        }

        public Timer.SoundTimer GetSoundTimer()
        {
            return soundTimer;
        }

        public byte GetStackPointer()
        {
            return stack.GetStackPointer();
        }

        public ushort[] GetStackData()
        {
            return stack.GetStackData();
        }



        private class Stack
        {
            private ushort[] values;
            private byte stackPointer;

            public Stack()
            {
                values = new ushort[16];
                stackPointer = 0;
            }

            public ushort[] GetStackData()
            {
                return values;
            }

            public byte GetStackPointer()
            {
                return stackPointer;
            }

            public void Push(ushort value)
            {
                if (stackPointer >= values.Length)
                {
                    throw new Exception("Stack overflow");
                }
                values[stackPointer] = value;
                stackPointer++;
            }

            public ushort Pop()
            {
                if(stackPointer == 0)
                {
                    throw new Exception("Stack underflow");
                }
                stackPointer--;
                return values[stackPointer];
            }
        }
    }
}
