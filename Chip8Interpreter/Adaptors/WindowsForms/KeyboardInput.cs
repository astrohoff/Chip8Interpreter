using System;
using Chip8Interpreter.Adaptors.Interfaces;
using Chip8Interpreter.Core;

namespace Chip8Interpreter.Adaptors.WindowsForms
{
    class KeyboardInput : KeypadInputAdaptor
    {
        public bool GetKeyState(Keypad.KeyID key)
        {
            return false;
        }
    }
}
