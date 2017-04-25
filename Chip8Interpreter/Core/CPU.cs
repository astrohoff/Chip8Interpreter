
namespace Chip8Interpreter.Core
{
    public class CPU
    {
        private byte[] generalRegisters;
        private ushort addressRegister;
        private Timer.DelayTimer delayRegister;
        private Timer.SoundTimer soundRegister;
        private ushort programCounter;
        private Stack stack;
        private Memory memory;
        private Display display;
        private Keypad keypad;

        public CPU(Memory memory, Display display, Keypad keypad)
        {
            generalRegisters = new byte[16];
            addressRegister = 0;
            delayRegister = new Timer.DelayTimer();
            soundRegister = new Timer.SoundTimer();
            programCounter = Memory.ProgramDataStart;
            stack = new Stack();
            this.memory = memory;
            this.display = display;
            this.keypad = keypad;
        }

        private class Stack
        {
            private ushort[] values;
            private byte stackPointer;

            public Stack()
            {
                values = new ushort[16];
                stackPointer = 15;
            }
        }
    }
}
