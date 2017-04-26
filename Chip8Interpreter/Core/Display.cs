using Chip8Interpreter.Adaptors.Interfaces;

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

        public bool GetPixelOn(int x, int y)
        {
            int pixelNumber = GetWrappedY(y) * Width + GetWrappedX(x);
            int byteAddress = pixelNumber / 8;
            int pixelBitmask = 0x80 >> (pixelNumber % 8);
            return (buffer[byteAddress] & pixelBitmask) != 0;
        }

        private static int GetWrappedX(int x)
        {
            return x % Width;
        }

        private static int GetWrappedY(int y)
        {
            return y % Height;
        }

        public bool DrawSprite(int baseX, int baseY, byte[] spriteData)
        {
            bool collision = false;
            for(int y = 0; y < spriteData.Length; y++)
            {
                for(int x = 0; x < 8; x++)
                {
                    bool pixelOn = (spriteData[y] & (0x80 >> x)) != 0;
                    collision |= DrawPixel(baseX + x, baseY + y, pixelOn);
                }
            }
            return collision;
        }

        private bool DrawPixel(int x, int y, bool pixelOn)
        {
            if (!pixelOn)
            {
                return false;
            }
            x = GetWrappedX(x);
            y = GetWrappedY(y);
            int pixelNumber = GetWrappedY(y) * Width + GetWrappedX(x);
            int byteAddress = pixelNumber / 8;
            int pixelBitmask = 0x80 >> (pixelNumber % 8);
            bool collision = pixelOn && ((buffer[byteAddress] & pixelBitmask) != 0);
            buffer[byteAddress] ^= (byte)pixelBitmask;
            return collision;
        }
    }
}
