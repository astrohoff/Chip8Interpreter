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
            opcodeLookup.Add(0x1000, new Instruction(JumpDirect));
            opcodeLookup.Add(0x2000, new Instruction(CallDirect));
            opcodeLookup.Add(0x3000, new Instruction(SkipIfEqualImmediate));
            opcodeLookup.Add(0x4000, new Instruction(SkipIfNotEqualImmediate));
            opcodeLookup.Add(0x5000, new Instruction(SkipIfEqualRegister));
            opcodeLookup.Add(0x6000, new Instruction(LoadImmediate));

            opcodeLookup.Add(0x8004, new Instruction(AddRegister));
            opcodeLookup.Add(0x8005, new Instruction(SubtractRegister));
            opcodeLookup.Add(0x8006, new Instruction(ShiftRight));
            opcodeLookup.Add(0x8007, new Instruction(SubractRegisterReversed));
            opcodeLookup.Add(0x800E, new Instruction(ShiftLeft));
            opcodeLookup.Add(0x9000, new Instruction(SkipIfNotEqualRegister));
            opcodeLookup.Add(0xA000, new Instruction(LoadAddressImmediate));
        }

        private void InitializeOpcodeDescriptionLookup()
        {
            opcodeDescriptionLookup = new Dictionary<int, string>();
            opcodeDescriptionLookup.Add(0x00E0, "Clear Screen");
            opcodeDescriptionLookup.Add(0x00EE, "Return");
            opcodeDescriptionLookup.Add(0x1000, "Jump Direct");
            opcodeDescriptionLookup.Add(0x2000, "Call Direct");
            opcodeDescriptionLookup.Add(0x3000, "Skip if equal immediate");
            opcodeDescriptionLookup.Add(0x4000, "Skip if not equal immediate");
            opcodeDescriptionLookup.Add(0x5000, "Skip if equal register");
            opcodeDescriptionLookup.Add(0x6000, "Load Immediate");

            opcodeDescriptionLookup.Add(0x8004, "Add Register");
            opcodeDescriptionLookup.Add(0x8005, "Subtract Register");
            opcodeDescriptionLookup.Add(0x8006, "Shift Right");
            opcodeDescriptionLookup.Add(0x8007, "Subtract Register Reversed");
            opcodeDescriptionLookup.Add(0x800E, "Shift Left");
            opcodeDescriptionLookup.Add(0x9000, "Skip if not equal register");
            opcodeDescriptionLookup.Add(0xA000, "Load Address Immediate");


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

        public void ClearScreen(ushort opcode)
        {
            display.ClearDisplay();
        }

        public void Return(ushort opcode)
        {
            programCounter = stack.Pop();
        }

        public void SystemJump(ushort opcode)
        {
            // Only relevent on original machines.
        }

        public void JumpDirect(ushort opcode)
        {
            programCounter = (ushort)(opcode & 0x0FFF);
        }

        public void CallDirect(ushort opcode)
        {
            stack.Push(programCounter);
            programCounter = (ushort)(opcode & 0x0FFF);
        }

        public void SkipIfEqualImmediate(ushort opcode)
        {
            if(generalRegisters[(opcode & 0x0F00) >> 8] == (opcode & 0x00FF))
            {
                programCounter += 2;
            }
        }

        public void SkipIfNotEqualImmediate(ushort opcode)
        {
            if(generalRegisters[((opcode & 0x0F00) >> 8)] != (opcode & 0x00FF))
            {
                programCounter += 2;
            }
        }

        public void SkipIfEqualRegister(ushort opcode)
        {
            if(generalRegisters[(opcode & 0x0F00) >> 8] == generalRegisters[(opcode & 0x00F0) >> 4])
            {
                programCounter += 2;
            }
        }

        public void LoadImmediate(ushort opcode)
        {
            generalRegisters[(opcode & 0x0F00) >> 8] = (byte)(opcode & 0x00FF);
        }


        public void AddRegister(ushort opcode)
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

        public void SubtractRegister(ushort opcode)
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

        public void ShiftRight(ushort opcode)
        {
            int srcRegisterIndex = (opcode & 0x00F0) >> 4;
            int dstRegisterIndex = (opcode & 0x0F00) >> 8;
            // Move least significant bit of source register to VF.
            generalRegisters[15] = (byte)(generalRegisters[srcRegisterIndex] & 0x01);
            generalRegisters[dstRegisterIndex] = (byte)(generalRegisters[srcRegisterIndex] >> 1);
        }

        public void SubractRegisterReversed(ushort opcode)
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

        public void ShiftLeft(ushort opcode)
        {
            int srcRegisterIndex = (opcode & 0x00F0) >> 4;
            int dstRegisterIndex = (opcode & 0x0F00) >> 8;
            // Move most significant bit of source register to VF.
            generalRegisters[15] = (byte)((generalRegisters[srcRegisterIndex] & 0x80) >> 7);
            generalRegisters[dstRegisterIndex] = (byte)(generalRegisters[srcRegisterIndex] << 1);
        }

        public void SkipIfNotEqualRegister(ushort opcode)
        {
            if(generalRegisters[(opcode & 0x0F00) >> 8] != generalRegisters[(opcode & 0x00F0) >> 4])
            {
                programCounter += 2;
            }
        }

        public void LoadAddressImmediate(ushort opcode)
        {
            addressRegister = (ushort)(opcode & 0x0FFF);
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
                    description = "Unimplemented";
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
