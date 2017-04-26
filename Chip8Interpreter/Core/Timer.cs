
using System;

namespace Chip8Interpreter.Core
{
    public abstract class Timer
    {
        protected byte time;

        protected abstract void OnActivate();
        protected abstract void OnDeactivate();

        public void SetValue(byte value)
        {
            time = value;
        }

        public byte GetValue()
        {
            return time;
        }

        public void Decrement()
        {
            if(time > 0)
            {
                time--;
            }
        }

        public class DelayTimer : Timer
        {
            public DelayTimer()
            {
            }

            protected override void OnActivate()
            {
                throw new NotImplementedException();
            }

            protected override void OnDeactivate()
            {
                throw new NotImplementedException();
            }
        }

        public class SoundTimer : Timer
        {
            public SoundTimer()
            {
                
            }

            protected override void OnActivate()
            {
                throw new NotImplementedException();
            }

            protected override void OnDeactivate()
            {
                throw new NotImplementedException();
            }
        }
    }
}
