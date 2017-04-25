
namespace Chip8Interpreter.Core.Types
{
    public class UInt4Operand
    {
        private int value;

        public UInt4Operand(int value)
        {
            this.value = value & 0x0F;
        }

        public byte GetValue()
        {
            return (byte)value;
        }
    }
}
