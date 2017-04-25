using Chip8Interpreter.Adaptors.Interfaces;

namespace Chip8Interpreter.Core
{
    public class Chip8System
    {
        private Memory memory;
        private Display display;
        private Keypad keypad;
        private CPU cpu;

        public Chip8System(byte[] programData, KeypadInputAdaptor inputAdaptor)
        {
            memory = new Memory(programData);
            display = new Display();
            keypad = new Keypad(inputAdaptor);
            cpu = new CPU(memory, display, keypad);
        }
    }
}
