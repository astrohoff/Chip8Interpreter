
namespace Chip8Interpreter.Core
{
    public class CPU
    {
        private byte[] generalRegisters;
        private ushort addressRegister;
        private Timer.DelayTimer delayRegister;
        private Timer.SoundTimer soundRegister;


        public CPU()
        {
            generalRegisters = new byte[16];
            addressRegister = 0;
            delayRegister = new Timer.DelayTimer();
            soundRegister = new Timer.SoundTimer();

        }
    }
}
