using System;

namespace Chip8Interpreter.Core
{
    public class Memory
    {
        public const ushort MemorySize = 0x1000;
        public const ushort ProgramDataStart = 0x200;
        public const ushort MaxProgramSize = MemorySize - ProgramDataStart;

        private byte[] data;

        public Memory()
        {
            data = new byte[MemorySize];
        }

        public Memory(byte[] fontData, byte[] programData)
        {
            data = new byte[MemorySize];
            Array.Copy(fontData, 0, data, 0, fontData.Length);
            LoadProgram(programData);
        }

        public void LoadProgram(byte[] programData)
        {
            if (programData.Length > MaxProgramSize)
            {
                throw new Exception("Max program size exceeded by " + (programData.Length - MaxProgramSize) + " bytes.");
            }
            Array.Copy(programData, 0, data, ProgramDataStart, programData.Length);
        }

        public byte[] GetData()
        {
            return data;
        }

        public byte ReadByte(int address)
        {
            return data[address];
        }

        public void WriteByte(byte value, int address)
        {
            data[address] = value;
        }
    }
}
