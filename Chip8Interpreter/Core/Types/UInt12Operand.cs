
namespace Chip8Interpreter.Core.Types
{
    public class UInt12Operand
    {
        private int value;

        public UInt12Operand(int value)
        {
            this.value = value & 0x0FFF;
        }

        public ushort GetValue()
        {
            return (ushort)value;
        }
        
    }
}
