
namespace Chip8Interpreter.Core.Types
{
    public class UInt8Operand
    {
        private int value;

        public UInt8Operand(int value)
        {
            this.value = value & 0xFF;
        }

        public byte GetValue()
        {
            return (byte)value;
        }
    }
}
