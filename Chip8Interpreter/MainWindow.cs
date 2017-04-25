using System;
using System.Windows.Forms;
using Chip8Interpreter.Core;
using Chip8Interpreter.Adaptors.WindowsForms;
using System.IO;

namespace Chip8Interpreter
{
    public partial class MainWindow : Form
    {
        private KeyboardInputAdaptor keyboardInput;
        private Chip8System chip8System;

        public MainWindow()
        {
            InitializeComponent();
            InitializeVerticalHex();
            InitializeMemoryView();
            InitializeGeneralRegistersView();
            InitializeSpecialRegistersView();
            InitializeStackView();
            SetManualControlsEnable(false);
            keyboardInput = new KeyboardInputAdaptor();
        }

        private void OnOpenButtonClick(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                openButtonLabel.Text = filePath;
                byte[] programData = File.ReadAllBytes(filePath);
                chip8System = new Chip8System(programData, keyboardInput);
                SetManualControlsEnable(true);
            }
            else
            {
                openButtonLabel.Text = "Select program file";
                SetManualControlsEnable(false);
            }
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

        private void SetManualControlsEnable(bool enable)
        {
            runCycleButton.Enabled = enable;
            decrementTimersButton.Enabled = enable;
            updateDisplayButton.Enabled = enable;
        }
    }
}
