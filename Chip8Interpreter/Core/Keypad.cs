using Chip8Interpreter.Adaptors.Interfaces;
using System;
using System.Threading;

namespace Chip8Interpreter.Core
{
    public class Keypad
    {
        public const int KeyCount = 16;

        private bool[] keyStates;
        private KeypadInputAdaptor inputAdaptor;
        
        public enum KeyID { Key0, Key1, Key2, Key3, Key4, Key5, Key6, Key7,
                            Key8, Key9, KeyA, KeyB, KeyC, KeyD, KeyE, KeyF, None = 0xFF };

        public Keypad(KeypadInputAdaptor inputAdaptor)
        {
            keyStates = new bool[KeyCount];
            this.inputAdaptor = inputAdaptor;
        }

        private KeyID GetInputState()
        {
            for(int i = 0; i < 16; i++)
            {
                if (inputAdaptor.GetKeyState((KeyID)i))
                {
                    return (KeyID)i;
                }
            }
            return KeyID.None;
        }

        public bool GetKeyState(byte keyValue)
        {
            return inputAdaptor.GetKeyState(GetKeyID(keyValue));
        }

        private byte GetKeyValue(KeyID keyID)
        {
            return (byte)keyID;
        }

        public byte GetNextKeyPress()
        {
            KeyID pressedKey = GetInputState();
            while(pressedKey == KeyID.None)
            {
                Thread.Sleep(100);
                pressedKey = GetInputState();
            }
            return GetKeyValue(pressedKey);
        }

        public byte GetCurrentKeyPress()
        {
            return GetKeyValue(GetInputState());
        }

        public KeyID GetKeyID(byte keyValue)
        {
            if(keyValue >= 0 && keyValue < 16 || keyValue == 0xFF)
            {
                return (KeyID)keyValue;
            }
            throw new Exception("Invalid key value: " + keyValue);
        }
    }
}
