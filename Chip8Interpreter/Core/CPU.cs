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

        private enum OperandFormat { Unknown };

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
            if (operandFormatLookup.ContainsKey(instructionRegister >> 3))
            {
                OperandFormat operandFormat = operandFormatLookup[instructionRegister >> 3];
                switch (operandFormat)
                {
                    default:
                        throw new Exception("Operand format not implemented.");
                } 
            }
            else
            {
                return OperandFormat.Unknown;
            }
        }

        private void Execute(OperandFormat operandFormat)
        {
            switch (operandFormat)
            {
                default:
                    throw new Exception("Unknown operand format");
            }
        }

        public static string GetInstructionDescription(ushort instruction)
        {
            switch (instruction & 0xF000)
            {
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
