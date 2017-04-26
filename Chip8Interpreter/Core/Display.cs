using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chip8Interpreter.Core
{
    public class Display
    {
        public const int Width = 64;
        public const int Height = 32;

        private byte[] buffer;

        public Display()
        {
            buffer = new byte[Width * Height / 8];
        }

        public void ClearDisplay()
        {
            for(int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = 0;
            }
        }

        public bool GetPixelOn()
        {
            return false;
        }
    }
}
