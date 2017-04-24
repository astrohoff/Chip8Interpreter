
namespace Chip8Interpreter.Core.Types
{
    public class UInt8
    {
        private int value;

        public UInt8(int value)
        {
            this.value = value & 0xFF;
        }
    }
}
