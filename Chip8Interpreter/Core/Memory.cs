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

        public Memory(byte[] programData)
        {
            data = new byte[MemorySize];
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
    }
}
