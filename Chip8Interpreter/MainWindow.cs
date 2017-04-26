using System;
using System.Windows.Forms;
using Chip8Interpreter.Core;
using Chip8Interpreter.Adaptors.WindowsForms;
using System.IO;
using System.Drawing;

namespace Chip8Interpreter
{
    public partial class MainWindow : Form
    {
        private KeyboardInputAdaptor keyboardInput;
        private Chip8System chip8System;

        public MainWindow()
        {
            keyboardInput = new KeyboardInputAdaptor();
            InitializeComponent();
            InitializeVerticalHex();
            InitializeMemoryView();
            InitializeGeneralRegistersView();
            InitializeSpecialRegistersView();
            InitializeStackView();
            InitializeNextInstruction();
            SetManualControlsEnable(false);
            if (File.Exists(Properties.Settings.Default.ProgramPath))
            {
                LoadFile(Properties.Settings.Default.ProgramPath);
            }
        }

        private void OnOpenButtonClick(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                Properties.Settings.Default.ProgramPath = openFileDialog1.FileName;
                Properties.Settings.Default.Save();
                LoadFile(openFileDialog1.FileName);
            }
            else
            {
                openButtonLabel.Text = "Select program file";
                SetManualControlsEnable(false);
                chip8System = null;
                InitializeMemoryView();
                InitializeGeneralRegistersView();
                InitializeSpecialRegistersView();
                InitializeStackView();
                InitializeNextInstruction();
            }
        }

        private void LoadFile(string filePath)
        {
            openButtonLabel.Text = filePath;
            byte[] programData = File.ReadAllBytes(filePath);
            chip8System = new Chip8System(programData, keyboardInput);
            SetManualControlsEnable(true);
            UpdateMemoryView();
            UpdateGeneralRegistersView();
            UpdateSpecialRegistersView();
            UpdateStackView();
            UpdateNextInstruction();
        }

        private void InitializeVerticalHex()
        {
            string text = "";
            for (int i = 0; i < Memory.MemorySize; i += 16)
            {
                text += i.ToString("X3");
                if (i < Memory.MemorySize - 1)
                {
                    text += "\r\n";
                }
            }
            verticalHexTextBox.Text = text;
            verticalHexTextBox.Size = verticalHexTextBox.PreferredSize;
        }

        private void InitializeMemoryView()
        {
            string text = "";
            for(int i = 0; i < Memory.MemorySize; i++)
            {
                text += "XX";
                if(i % 16 < 15)
                {
                    text += " ";
                }
                else if (i < (Memory.MemorySize - 1))
                {
                    text += "\r\n";
                }
            }
            memoryTextBox.Text = text;
            memoryTextBox.Size = memoryTextBox.PreferredSize;
        }

        private void InitializeGeneralRegistersView()
        {
            string text = "";
            for(int i = 0; i < 8; i++)
            {
                text += "V" + i + ":XX";
                text += " V" + (i + 8).ToString("X1") + ":XX";
                if(i < 7)
                {
                    text += "\r\n";
                }
            }
            generalRegistersTextBox.Text = text;
        }

        private void InitializeSpecialRegistersView()
        {
            string text = "";
            text += "PC:XXXX\r\n";
            text += "SP:XXXX\r\n";
            text += "I:XXXX\r\n";
            text += "Delay:XX\r\n";
            text += "Sound:XX";
            specialRegistersTextBox.Text = text;
        }

        private void InitializeStackView()
        {
            string text = "";
            for(int i = 15; i >= 0; i--)
            {
                text += "S" + i.ToString("X1") + ":XX";
                if(i > 0)
                {
                    text += "\r\n";
                }
            }
            stackTextBox.Text = text;
        }

        private void InitializeNextInstruction()
        {
            nextInstructionValueLabel.Text = "XXXX";
            nextInstructionTextBox.Text = "";
        }

        private void SetManualControlsEnable(bool enable)
        {
            runCycleButton.Enabled = enable;
            decrementTimersButton.Enabled = enable;
            updateDisplayButton.Enabled = enable;
        }

        private void UpdateMemoryView()
        {
            byte[] memoryData = chip8System.GetMemory().GetData();
            string text = "";
            for (int i = 0; i < Memory.MemorySize; i++)
            {
                text += memoryData[i].ToString("X2");
                if (i % 16 < 15)
                {
                    text += " ";
                }
                else if (i < (Memory.MemorySize - 1))
                {
                    text += "\r\n";
                }
            }
            memoryTextBox.Text = text;
        }

        private void UpdateGeneralRegistersView()
        {
            byte[] generalRegisters = chip8System.GetCPU().GetGeneralRegisters();
            string text = "";
            for (int i = 0; i < 8; i++)
            {
                text += "V" + i + ":" + generalRegisters[i].ToString("X2");
                text += " V" + (i + 8).ToString("X1") + ":" + generalRegisters[i + 8].ToString("X2");
                if (i < 7)
                {
                    text += "\r\n";
                }
            }
            generalRegistersTextBox.Text = text;
        }

        private void UpdateSpecialRegistersView()
        {
            CPU cpu = chip8System.GetCPU();
            string text = "";
            text += "PC:" + cpu.GetProgramCounter().ToString("X4") + "\r\n";
            text += "SP:" + cpu.GetStackPointer().ToString("X2") + "\r\n";
            text += "I:" + cpu.GetAddressRegister().ToString("X4") + "\r\n";
            text += "Delay:" + cpu.GetDelayTimer().GetValue().ToString("X2") + "\r\n";
            text += "Sound:" + cpu.GetSoundTimer().GetValue().ToString("X2");
            specialRegistersTextBox.Text = text;
        }

        private void UpdateStackView()
        {
            ushort[] stackData = chip8System.GetCPU().GetStackData();
            string text = "";
            for (int i = 15; i >= 0; i--)
            {
                text += "S" + i.ToString("X1") + ":" + stackData[i].ToString("X4");
                if (i > 0)
                {
                    text += "\r\n";
                }
            }
            stackTextBox.Text = text;
        }

        private void UpdateNextInstruction()
        {
            Memory memory = chip8System.GetMemory();
            ushort offset = chip8System.GetCPU().GetProgramCounter();
            ushort instruction = (ushort)((memory.ReadByte(offset) << 8) | (memory.ReadByte(offset + 1)));
            nextInstructionValueLabel.Text = instruction.ToString("X4");
            nextInstructionTextBox.Text = chip8System.GetCPU().GetInstructionDescription(instruction);
        }

        private void OnRunCycleButtonClick(object sender, EventArgs e)
        {
            chip8System.GetCPU().RunCycle();
            UpdateMemoryView();
            UpdateGeneralRegistersView();
            UpdateSpecialRegistersView();
            UpdateStackView();
            UpdateNextInstruction();
            UpdateDisplayView();
        }

        private void OnDecrementTimersButtonClick(object sender, EventArgs e)
        {
            chip8System.GetCPU().GetDelayTimer().Decrement();
            chip8System.GetCPU().GetSoundTimer().Decrement();
        }

        private void UpdateDisplayView()
        {
            Display display = chip8System.GetDisplay();
            Bitmap displayBitmap = new Bitmap(Display.Width, Display.Height);
            for (int y = 0; y < Display.Height; y++)
            {
                for (int x = 0; x < Display.Width; x++)
                {
                    Color pixelColor;
                    if (display.GetPixelOn(x, y))
                    {
                        pixelColor = Color.White;
                    }
                    else
                    {
                        pixelColor = Color.Black;
                    }
                    displayBitmap.SetPixel(x, y, pixelColor);
                }
            }
            displayImagePanel.BackgroundImage = displayBitmap;
        }

        private void OnUpdateDisplayButtonClick(object sender, EventArgs e)
        {
            UpdateDisplayView();
        }
    }
}
