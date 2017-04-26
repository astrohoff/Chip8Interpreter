using Chip8Interpreter.Adaptors.Interfaces;
using Chip8Interpreter.Core;
using System.Windows.Input;
using System;

namespace Chip8Interpreter.Adaptors.WindowsForms
{
    public class KeyboardInputAdaptor : KeypadInputAdaptor
    {
        private static KeyMapping[] keyMappings = new KeyMapping[]
        {
            new KeyMapping(Keypad.KeyID.Key1, Key.D2), new KeyMapping(Keypad.KeyID.Key2, Key.D3),
            new KeyMapping(Keypad.KeyID.Key3, Key.D4), new KeyMapping(Keypad.KeyID.KeyC, Key.D5),
            new KeyMapping(Keypad.KeyID.Key4, Key.Q), new KeyMapping(Keypad.KeyID.Key5, Key.W),
            new KeyMapping(Keypad.KeyID.Key6, Key.E), new KeyMapping(Keypad.KeyID.KeyD, Key.R),
            new KeyMapping(Keypad.KeyID.Key7, Key.A), new KeyMapping(Keypad.KeyID.Key8, Key.S),
            new KeyMapping(Keypad.KeyID.Key9, Key.D), new KeyMapping(Keypad.KeyID.KeyE, Key.F),
            new KeyMapping(Keypad.KeyID.KeyA, Key.Z), new KeyMapping(Keypad.KeyID.Key0, Key.X),
            new KeyMapping(Keypad.KeyID.KeyB, Key.C), new KeyMapping(Keypad.KeyID.KeyF, Key.V)
        };

        public KeyboardInputAdaptor()
        {

        }

        public bool GetKeyState(Keypad.KeyID keypadKey)
        {
            Key keyboardKey = GetKeyboardKey(keypadKey);
            return Keyboard.IsKeyDown(keyboardKey);
        }

        private Key GetKeyboardKey(Keypad.KeyID keypadKey)
        {
            for(int i = 0; i < keyMappings.Length; i++)
            {
                if(keyMappings[i].GetKeypadID() == keypadKey)
                {
                    return keyMappings[i].GetKeyboardID();
                }
            }
            throw new Exception("Invalid keypad key: " + ((int)keypadKey).ToString("X"));
        }

        private class KeyMapping
        {
            private Keypad.KeyID keypadID;
            private Key keyboardID;

            public KeyMapping(Keypad.KeyID keypadID, Key keyboardID)
            {
                this.keypadID = keypadID;
                this.keyboardID = keyboardID;
            }

            public Keypad.KeyID GetKeypadID()
            {
                return keypadID;
            }

            public Key GetKeyboardID()
            {
                return keyboardID;
            }
        }
    }
}
