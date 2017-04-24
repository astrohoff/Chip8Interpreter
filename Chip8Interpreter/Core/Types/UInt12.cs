
namespace Chip8Interpreter.Core.Types
{
    public class UInt12
    {
        private int value;

        public UInt12(int value)
        {
            this.value = value & 0x0FFF;
        }
        
    }
}
