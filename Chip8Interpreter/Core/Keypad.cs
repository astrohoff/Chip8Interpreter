
namespace Chip8Interpreter.Core
{
    public class Keypad
    {
        public const int KeyCount = 16;

        private bool[] keyStates;

        public Keypad()
        {
            keyStates = new bool[KeyCount];
        }
    }
}
