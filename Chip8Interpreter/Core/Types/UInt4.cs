
namespace Chip8Interpreter.Core.Types
{
    public class UInt4
    {
        private int value;

        public UInt4(int value)
        {
            this.value = value & 0x0F;
        }
    }
}
