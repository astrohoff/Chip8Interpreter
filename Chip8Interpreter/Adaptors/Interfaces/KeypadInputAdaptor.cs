using Chip8Interpreter.Core;

namespace Chip8Interpreter.Adaptors.Interfaces
{
    public interface KeypadInputAdaptor
    {
        bool GetKeyState(Keypad.KeyID key);
    }
}
