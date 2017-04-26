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

        // Internal decode aiding variables that shouldn't be important externally.
        private ushort instructionRegister;
        private UInt12Operand u12Operand;
        private UInt8Operand u8Operand;
        private UInt4Operand u4Operand0, u4Operand1, u4Operand2;
        private Dictionary<int, OperandFormat> operandFormatLookup;

        private enum OperandFormat { Unknown, None, u12, u4u8, u4, u4u4, u4u4u4 };

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

            instructionRegister = 0;
            u12Operand = new UInt12Operand(0);
            u8Operand = new UInt8Operand(0);
            u4Operand0 = new UInt4Operand(0);
            u4Operand1 = new UInt4Operand(0);
            u4Operand2 = new UInt4Operand(0);
            operandFormatLookup = new Dictionary<int, OperandFormat>();
            InitializeOperandFormatLookup();
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


        public void RunCycle()
        {
            Fetch();
            OperandFormat operandFormat =  Decode();
            Execute(operandFormat);
        }

        private void Fetch()
        {
            byte[] memoryData = memory.GetData();
            instructionRegister = (ushort)((memoryData[programCounter] << 8) | memoryData[programCounter + 1]);
            programCounter += 2;
        }

        private OperandFormat Decode()
        {
            int first4Bits = instructionRegister >> 12;
            if (operandFormatLookup.ContainsKey(first4Bits))
            {
                OperandFormat operandFormat = operandFormatLookup[first4Bits];
                switch (operandFormat)
                {
                    case OperandFormat.None:
                        break;
                    case OperandFormat.u12:
                        u12Operand = new UInt12Operand(instructionRegister & 0x0FFF);
                        break;
                    case OperandFormat.u4:
                        u4Operand0 = new UInt4Operand((instructionRegister >> 8) & 0x000F);
                        break;
                    case OperandFormat.u4u4:
                        u4Operand0 = new UInt4Operand((instructionRegister >> 8) & 0x000F);
                        u4Operand1 = new UInt4Operand((instructionRegister >> 4) & 0x000F);
                        break;
                    case OperandFormat.u4u4u4:
                        u4Operand0 = new UInt4Operand((instructionRegister >> 8) & 0x000F);
                        u4Operand1 = new UInt4Operand((instructionRegister >> 4) & 0x000F);
                        u4Operand2 = new UInt4Operand(instructionRegister & 0x000F);
                        break;
                    case OperandFormat.u4u8:
                        u4Operand0 = new UInt4Operand((instructionRegister >> 8) & 0x000F);
                        u8Operand = new UInt8Operand(instructionRegister & 0x00FF);
                        break;
                    default:
                        throw new Exception("Operand format " + operandFormat.ToString() + " not implemented.");
                }
                return operandFormat;
            }
            else
            {
                if(first4Bits == 0)
                {
                    if(instructionRegister == 0x00E0 || instructionRegister == 0x00EE)
                    {
                        return OperandFormat.None;
                    }
                    else
                    {
                        u12Operand = new UInt12Operand(instructionRegister & 0x0FFF);
                        return OperandFormat.u12;
                    }
                }
                else
                {
                    throw new Exception("Unknown operand format.");
                }
            }
        }

        private void Execute(OperandFormat operandFormat)
        {
            switch (operandFormat)
            {
                case OperandFormat.None:
                    Execute();
                    break;
                case OperandFormat.u12:
                    Execute(u12Operand);
                    break;
                case OperandFormat.u4:
                    Execute(u4Operand0);
                    break;
                case OperandFormat.u4u4:
                    Execute(u4Operand0, u4Operand1);
                    break;
                case OperandFormat.u4u4u4:
                    Execute(u4Operand0, u4Operand1, u4Operand2);
                    break;
                case OperandFormat.u4u8:
                    Execute(u4Operand0, u8Operand);
                    break;
                default:
                    throw new Exception("Unimplemented operand format " + operandFormat.ToString());
            }
        }

        private void Execute(UInt12Operand u12Operand)
        {
            switch(instructionRegister & 0xF000)
            {
                default:
                    throw new Exception("Unimplemented instruction " + instructionRegister.ToString("X4"));
            }
        }

        private void Execute(UInt4Operand u4Operand)
        {
            switch(instructionRegister & 0xF000)
            {
                default:
                    throw new Exception("Unimplemented instruction " + instructionRegister.ToString("X4"));
            }
        }

        private void Execute()
        {
            switch (instructionRegister)
            {
                case 0x00E0:
                    ClearScreen();
                    break;
                default:
                    throw new Exception("Unimplemented instruction " + instructionRegister.ToString("X4"));
            }
        }

        private void Execute(UInt4Operand u4Operand0, UInt4Operand u4Operand1)
        {
            switch (instructionRegister & 0xF000)
            {
                default:
                    throw new Exception("Unimplemented instruction " + instructionRegister.ToString("X4"));
            }
        }

        private void Execute(UInt4Operand u4Operand0, UInt4Operand u4Operand1, UInt4Operand u4Operand2)
        {
            switch(instructionRegister & 0xF000)
            {
                default:
                    throw new Exception("Unimplemented instruction " + instructionRegister.ToString("X4"));
            }
        }

        private void Execute(UInt4Operand u4Operand, UInt8Operand u8Operand)
        {
            switch(instructionRegister & 0xF000)
            {
                case 0x6000:
                    LoadImmediate(u4Operand, u8Operand);
                    break;
                default:
                    throw new Exception("Unimplemented instruction " + instructionRegister.ToString("X4"));
            }
        }

        private void InitializeOperandFormatLookup()
        {
            operandFormatLookup.Add(1, OperandFormat.u4u4u4);
            operandFormatLookup.Add(2, OperandFormat.u4u4u4);
            operandFormatLookup.Add(3, OperandFormat.u4u8);
            operandFormatLookup.Add(4, OperandFormat.u4u8);
            operandFormatLookup.Add(5, OperandFormat.u4u4);
            operandFormatLookup.Add(6, OperandFormat.u4u8);
            operandFormatLookup.Add(7, OperandFormat.u4u8);
            operandFormatLookup.Add(8, OperandFormat.u4u4);
            operandFormatLookup.Add(9, OperandFormat.u4u4);
            operandFormatLookup.Add(0xA, OperandFormat.u12);
            operandFormatLookup.Add(0xB, OperandFormat.u12);
            operandFormatLookup.Add(0xC, OperandFormat.u4u8);
            operandFormatLookup.Add(0xD, OperandFormat.u4u4u4);
            operandFormatLookup.Add(0xE, OperandFormat.u4);
            operandFormatLookup.Add(0xF, OperandFormat.u4);
        }


        // Instructions.

        private void ClearScreen()
        {
            display.ClearDisplay();
        }

        private void LoadImmediate(UInt4Operand u4Operand, UInt8Operand u8Operand)
        {
            generalRegisters[u4Operand.GetValue()] = u8Operand.GetValue();
        }



        public static string GetInstructionDescription(ushort instruction)
        {
            switch (instruction & 0xF000)
            {
                case 0x0000:
                    switch(instruction & 0x00FF)
                    {
                        case 0x00E0:
                            return "Clear Screen";
                        case 0x00EE:
                            return "Return";
                        default:
                            return "System Jump";
                    }
                default:
                    return "Unimplemented";
            }
        }

        private class Stack
        {
            private ushort[] values;
            private byte stackPointer;

            public Stack()
            {
                values = new ushort[16];
                stackPointer = 15;
            }

            public ushort[] GetStackData()
            {
                return values;
            }

            public byte GetStackPointer()
            {
                return stackPointer;
            }
        }
    }
}
