
namespace Chip8Interpreter.Core
{
    public class Memory
    {
        public const ushort ByteCount = 0x1000;

        private byte[] data;

        public Memory()
        {
            data = new byte[Memory.ByteCount];
        }
    }
}
