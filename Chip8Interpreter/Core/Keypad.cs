using Chip8Interpreter.Adaptors.Interfaces;

namespace Chip8Interpreter.Core
{
    public class Keypad
    {
        public const int KeyCount = 16;

        private bool[] keyStates;
        private KeypadInputAdaptor inputAdaptor;
        
        public enum KeyID { Key0, Key1, Key2, Key3, Key4, Key5, Key6, Key7,
                            Key8, Key9, KeyA, KeyB, KeyC, KeyD, KeyE, KeyF };

        public Keypad(KeypadInputAdaptor inputAdaptor)
        {
            keyStates = new bool[KeyCount];
            this.inputAdaptor = inputAdaptor;
        }
    }
}
