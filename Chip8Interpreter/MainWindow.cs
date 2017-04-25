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
            keyboardInput = new KeyboardInputAdaptor();
        }

        private void OnOpenButtonClick(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                byte[] programData = File.ReadAllBytes(filePath);
                chip8System = new Chip8System(programData, keyboardInput);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
