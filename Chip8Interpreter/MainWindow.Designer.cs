namespace Chip8Interpreter
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openButton = new System.Windows.Forms.Button();
            this.openButtonLabel = new System.Windows.Forms.Label();
            this.fileSelectPanel = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cpuStatusPanel = new System.Windows.Forms.Panel();
            this.nextInstructionPanel = new System.Windows.Forms.Panel();
            this.nextInstructionTextBox = new System.Windows.Forms.TextBox();
            this.nextInstructionValueLabel = new System.Windows.Forms.Label();
            this.nextInstructionLabel = new System.Windows.Forms.Label();
            this.stackPanel = new System.Windows.Forms.Panel();
            this.stackTextBox = new System.Windows.Forms.TextBox();
            this.stackLabel = new System.Windows.Forms.Label();
            this.specialRegistersPanel = new System.Windows.Forms.Panel();
            this.specialRegistersTextBox = new System.Windows.Forms.TextBox();
            this.specialRegistersLabel = new System.Windows.Forms.Label();
            this.generalRegistersPanel = new System.Windows.Forms.Panel();
            this.generalRegistersTextBox = new System.Windows.Forms.TextBox();
            this.generalRegistersLabel = new System.Windows.Forms.Label();
            this.cpuStatusLabel = new System.Windows.Forms.Label();
            this.memoryPanel = new System.Windows.Forms.Panel();
            this.memoryViewport = new System.Windows.Forms.Panel();
            this.memoryTextBox = new System.Windows.Forms.TextBox();
            this.verticalHexTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.memoryLabel = new System.Windows.Forms.Label();
            this.displayPanel = new System.Windows.Forms.Panel();
            this.displayImagePanel = new System.Windows.Forms.Panel();
            this.displayLabel = new System.Windows.Forms.Label();
            this.manualControlsPanel = new System.Windows.Forms.Panel();
            this.updateDisplayButton = new System.Windows.Forms.Button();
            this.decrementTimersButton = new System.Windows.Forms.Button();
            this.runCycleButton = new System.Windows.Forms.Button();
            this.manualControlsLabel = new System.Windows.Forms.Label();
            this.fileSelectPanel.SuspendLayout();
            this.cpuStatusPanel.SuspendLayout();
            this.nextInstructionPanel.SuspendLayout();
            this.stackPanel.SuspendLayout();
            this.specialRegistersPanel.SuspendLayout();
            this.generalRegistersPanel.SuspendLayout();
            this.memoryPanel.SuspendLayout();
            this.memoryViewport.SuspendLayout();
            this.displayPanel.SuspendLayout();
            this.manualControlsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(3, 3);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 0;
            this.openButton.Text = "Open File";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.OnOpenButtonClick);
            // 
            // openButtonLabel
            // 
            this.openButtonLabel.AutoSize = true;
            this.openButtonLabel.Location = new System.Drawing.Point(84, 8);
            this.openButtonLabel.Name = "openButtonLabel";
            this.openButtonLabel.Size = new System.Drawing.Size(94, 13);
            this.openButtonLabel.TabIndex = 1;
            this.openButtonLabel.Text = "Select program file";
            // 
            // fileSelectPanel
            // 
            this.fileSelectPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileSelectPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fileSelectPanel.Controls.Add(this.openButton);
            this.fileSelectPanel.Controls.Add(this.openButtonLabel);
            this.fileSelectPanel.Location = new System.Drawing.Point(12, 12);
            this.fileSelectPanel.Name = "fileSelectPanel";
            this.fileSelectPanel.Size = new System.Drawing.Size(729, 31);
            this.fileSelectPanel.TabIndex = 2;
            // 
            // cpuStatusPanel
            // 
            this.cpuStatusPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cpuStatusPanel.Controls.Add(this.nextInstructionPanel);
            this.cpuStatusPanel.Controls.Add(this.stackPanel);
            this.cpuStatusPanel.Controls.Add(this.specialRegistersPanel);
            this.cpuStatusPanel.Controls.Add(this.generalRegistersPanel);
            this.cpuStatusPanel.Controls.Add(this.cpuStatusLabel);
            this.cpuStatusPanel.Location = new System.Drawing.Point(12, 49);
            this.cpuStatusPanel.Name = "cpuStatusPanel";
            this.cpuStatusPanel.Size = new System.Drawing.Size(284, 285);
            this.cpuStatusPanel.TabIndex = 3;
            // 
            // nextInstructionPanel
            // 
            this.nextInstructionPanel.Controls.Add(this.nextInstructionTextBox);
            this.nextInstructionPanel.Controls.Add(this.nextInstructionValueLabel);
            this.nextInstructionPanel.Controls.Add(this.nextInstructionLabel);
            this.nextInstructionPanel.Location = new System.Drawing.Point(3, 175);
            this.nextInstructionPanel.Name = "nextInstructionPanel";
            this.nextInstructionPanel.Size = new System.Drawing.Size(200, 100);
            this.nextInstructionPanel.TabIndex = 4;
            // 
            // nextInstructionTextBox
            // 
            this.nextInstructionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nextInstructionTextBox.Location = new System.Drawing.Point(6, 39);
            this.nextInstructionTextBox.Multiline = true;
            this.nextInstructionTextBox.Name = "nextInstructionTextBox";
            this.nextInstructionTextBox.Size = new System.Drawing.Size(191, 58);
            this.nextInstructionTextBox.TabIndex = 2;
            this.nextInstructionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nextInstructionValueLabel
            // 
            this.nextInstructionValueLabel.AutoSize = true;
            this.nextInstructionValueLabel.Location = new System.Drawing.Point(81, 23);
            this.nextInstructionValueLabel.Name = "nextInstructionValueLabel";
            this.nextInstructionValueLabel.Size = new System.Drawing.Size(35, 13);
            this.nextInstructionValueLabel.TabIndex = 1;
            this.nextInstructionValueLabel.Text = "XXXX";
            // 
            // nextInstructionLabel
            // 
            this.nextInstructionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nextInstructionLabel.Location = new System.Drawing.Point(3, 3);
            this.nextInstructionLabel.Margin = new System.Windows.Forms.Padding(3);
            this.nextInstructionLabel.Name = "nextInstructionLabel";
            this.nextInstructionLabel.Size = new System.Drawing.Size(194, 17);
            this.nextInstructionLabel.TabIndex = 0;
            this.nextInstructionLabel.Text = "Next Instruction:";
            this.nextInstructionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // stackPanel
            // 
            this.stackPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.stackPanel.Controls.Add(this.stackTextBox);
            this.stackPanel.Controls.Add(this.stackLabel);
            this.stackPanel.Location = new System.Drawing.Point(213, 22);
            this.stackPanel.Name = "stackPanel";
            this.stackPanel.Size = new System.Drawing.Size(65, 258);
            this.stackPanel.TabIndex = 3;
            // 
            // stackTextBox
            // 
            this.stackTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stackTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.stackTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stackTextBox.Location = new System.Drawing.Point(3, 22);
            this.stackTextBox.Multiline = true;
            this.stackTextBox.Name = "stackTextBox";
            this.stackTextBox.ReadOnly = true;
            this.stackTextBox.Size = new System.Drawing.Size(57, 231);
            this.stackTextBox.TabIndex = 5;
            this.stackTextBox.Text = "S0:XXXX\r\n1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\na\r\nb\r\nc\r\nd\r\ne\r\nf";
            // 
            // stackLabel
            // 
            this.stackLabel.Location = new System.Drawing.Point(3, 3);
            this.stackLabel.Margin = new System.Windows.Forms.Padding(3);
            this.stackLabel.Name = "stackLabel";
            this.stackLabel.Size = new System.Drawing.Size(57, 13);
            this.stackLabel.TabIndex = 0;
            this.stackLabel.Text = "Stack";
            this.stackLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // specialRegistersPanel
            // 
            this.specialRegistersPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.specialRegistersPanel.Controls.Add(this.specialRegistersTextBox);
            this.specialRegistersPanel.Controls.Add(this.specialRegistersLabel);
            this.specialRegistersPanel.Location = new System.Drawing.Point(110, 22);
            this.specialRegistersPanel.Name = "specialRegistersPanel";
            this.specialRegistersPanel.Size = new System.Drawing.Size(97, 103);
            this.specialRegistersPanel.TabIndex = 2;
            // 
            // specialRegistersTextBox
            // 
            this.specialRegistersTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.specialRegistersTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.specialRegistersTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.specialRegistersTextBox.Location = new System.Drawing.Point(3, 22);
            this.specialRegistersTextBox.Multiline = true;
            this.specialRegistersTextBox.Name = "specialRegistersTextBox";
            this.specialRegistersTextBox.ReadOnly = true;
            this.specialRegistersTextBox.Size = new System.Drawing.Size(89, 76);
            this.specialRegistersTextBox.TabIndex = 2;
            this.specialRegistersTextBox.Text = "d\r\nd\r\nd\r\nd\r\ndelay:xx";
            // 
            // specialRegistersLabel
            // 
            this.specialRegistersLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.specialRegistersLabel.AutoSize = true;
            this.specialRegistersLabel.Location = new System.Drawing.Point(3, 3);
            this.specialRegistersLabel.Margin = new System.Windows.Forms.Padding(3);
            this.specialRegistersLabel.Name = "specialRegistersLabel";
            this.specialRegistersLabel.Size = new System.Drawing.Size(89, 13);
            this.specialRegistersLabel.TabIndex = 0;
            this.specialRegistersLabel.Text = "Special Registers";
            // 
            // generalRegistersPanel
            // 
            this.generalRegistersPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.generalRegistersPanel.Controls.Add(this.generalRegistersTextBox);
            this.generalRegistersPanel.Controls.Add(this.generalRegistersLabel);
            this.generalRegistersPanel.Location = new System.Drawing.Point(3, 22);
            this.generalRegistersPanel.Name = "generalRegistersPanel";
            this.generalRegistersPanel.Size = new System.Drawing.Size(101, 147);
            this.generalRegistersPanel.TabIndex = 1;
            // 
            // generalRegistersTextBox
            // 
            this.generalRegistersTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.generalRegistersTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.generalRegistersTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generalRegistersTextBox.Location = new System.Drawing.Point(4, 22);
            this.generalRegistersTextBox.Multiline = true;
            this.generalRegistersTextBox.Name = "generalRegistersTextBox";
            this.generalRegistersTextBox.ReadOnly = true;
            this.generalRegistersTextBox.Size = new System.Drawing.Size(92, 120);
            this.generalRegistersTextBox.TabIndex = 1;
            this.generalRegistersTextBox.Text = "VX:XX  VX:XX\r\nV\r\nV\r\nV\r\nV\r\nV\r\nV\r\nV";
            // 
            // generalRegistersLabel
            // 
            this.generalRegistersLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.generalRegistersLabel.Location = new System.Drawing.Point(2, 3);
            this.generalRegistersLabel.Margin = new System.Windows.Forms.Padding(3);
            this.generalRegistersLabel.Name = "generalRegistersLabel";
            this.generalRegistersLabel.Size = new System.Drawing.Size(94, 13);
            this.generalRegistersLabel.TabIndex = 0;
            this.generalRegistersLabel.Text = "General Registers";
            this.generalRegistersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cpuStatusLabel
            // 
            this.cpuStatusLabel.Location = new System.Drawing.Point(3, 3);
            this.cpuStatusLabel.Margin = new System.Windows.Forms.Padding(3);
            this.cpuStatusLabel.Name = "cpuStatusLabel";
            this.cpuStatusLabel.Size = new System.Drawing.Size(300, 13);
            this.cpuStatusLabel.TabIndex = 0;
            this.cpuStatusLabel.Text = "CPU Status";
            this.cpuStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // memoryPanel
            // 
            this.memoryPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.memoryPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.memoryPanel.Controls.Add(this.memoryViewport);
            this.memoryPanel.Controls.Add(this.label1);
            this.memoryPanel.Controls.Add(this.memoryLabel);
            this.memoryPanel.Location = new System.Drawing.Point(322, 49);
            this.memoryPanel.Name = "memoryPanel";
            this.memoryPanel.Size = new System.Drawing.Size(419, 401);
            this.memoryPanel.TabIndex = 4;
            // 
            // memoryViewport
            // 
            this.memoryViewport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memoryViewport.AutoScroll = true;
            this.memoryViewport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.memoryViewport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.memoryViewport.Controls.Add(this.memoryTextBox);
            this.memoryViewport.Controls.Add(this.verticalHexTextBox);
            this.memoryViewport.Location = new System.Drawing.Point(6, 39);
            this.memoryViewport.Name = "memoryViewport";
            this.memoryViewport.Size = new System.Drawing.Size(408, 357);
            this.memoryViewport.TabIndex = 2;
            // 
            // memoryTextBox
            // 
            this.memoryTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.memoryTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memoryTextBox.Location = new System.Drawing.Point(42, 3);
            this.memoryTextBox.Multiline = true;
            this.memoryTextBox.Name = "memoryTextBox";
            this.memoryTextBox.ReadOnly = true;
            this.memoryTextBox.Size = new System.Drawing.Size(340, 318);
            this.memoryTextBox.TabIndex = 1;
            this.memoryTextBox.Text = "00 11 22 33 44 55 66 77 88 99 aa bb cc dd ee ff";
            // 
            // verticalHexTextBox
            // 
            this.verticalHexTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.verticalHexTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.verticalHexTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verticalHexTextBox.Location = new System.Drawing.Point(4, 3);
            this.verticalHexTextBox.Multiline = true;
            this.verticalHexTextBox.Name = "verticalHexTextBox";
            this.verticalHexTextBox.ReadOnly = true;
            this.verticalHexTextBox.Size = new System.Drawing.Size(32, 318);
            this.verticalHexTextBox.TabIndex = 0;
            this.verticalHexTextBox.Text = "000";
            this.verticalHexTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(341, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // memoryLabel
            // 
            this.memoryLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memoryLabel.Location = new System.Drawing.Point(3, 3);
            this.memoryLabel.Margin = new System.Windows.Forms.Padding(3);
            this.memoryLabel.Name = "memoryLabel";
            this.memoryLabel.Size = new System.Drawing.Size(411, 13);
            this.memoryLabel.TabIndex = 0;
            this.memoryLabel.Text = "Memory Status";
            this.memoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // displayPanel
            // 
            this.displayPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.displayPanel.Controls.Add(this.displayImagePanel);
            this.displayPanel.Controls.Add(this.displayLabel);
            this.displayPanel.Location = new System.Drawing.Point(12, 340);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(82, 59);
            this.displayPanel.TabIndex = 5;
            // 
            // displayImagePanel
            // 
            this.displayImagePanel.BackColor = System.Drawing.Color.Gray;
            this.displayImagePanel.Location = new System.Drawing.Point(8, 20);
            this.displayImagePanel.Name = "displayImagePanel";
            this.displayImagePanel.Size = new System.Drawing.Size(64, 32);
            this.displayImagePanel.TabIndex = 1;
            // 
            // displayLabel
            // 
            this.displayLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.displayLabel.Location = new System.Drawing.Point(3, 0);
            this.displayLabel.Name = "displayLabel";
            this.displayLabel.Size = new System.Drawing.Size(74, 17);
            this.displayLabel.TabIndex = 0;
            this.displayLabel.Text = "Display";
            this.displayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // manualControlsPanel
            // 
            this.manualControlsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.manualControlsPanel.Controls.Add(this.updateDisplayButton);
            this.manualControlsPanel.Controls.Add(this.decrementTimersButton);
            this.manualControlsPanel.Controls.Add(this.runCycleButton);
            this.manualControlsPanel.Controls.Add(this.manualControlsLabel);
            this.manualControlsPanel.Location = new System.Drawing.Point(100, 340);
            this.manualControlsPanel.Name = "manualControlsPanel";
            this.manualControlsPanel.Size = new System.Drawing.Size(216, 79);
            this.manualControlsPanel.TabIndex = 6;
            // 
            // updateDisplayButton
            // 
            this.updateDisplayButton.Location = new System.Drawing.Point(4, 51);
            this.updateDisplayButton.Name = "updateDisplayButton";
            this.updateDisplayButton.Size = new System.Drawing.Size(97, 23);
            this.updateDisplayButton.TabIndex = 3;
            this.updateDisplayButton.Text = "Update Display";
            this.updateDisplayButton.UseVisualStyleBackColor = true;
            this.updateDisplayButton.Click += new System.EventHandler(this.OnUpdateDisplayButtonClick);
            // 
            // decrementTimersButton
            // 
            this.decrementTimersButton.Location = new System.Drawing.Point(108, 21);
            this.decrementTimersButton.Name = "decrementTimersButton";
            this.decrementTimersButton.Size = new System.Drawing.Size(101, 23);
            this.decrementTimersButton.TabIndex = 2;
            this.decrementTimersButton.Text = "Decrement Timers";
            this.decrementTimersButton.UseVisualStyleBackColor = true;
            this.decrementTimersButton.Click += new System.EventHandler(this.OnDecrementTimersButtonClick);
            // 
            // runCycleButton
            // 
            this.runCycleButton.Location = new System.Drawing.Point(4, 21);
            this.runCycleButton.Name = "runCycleButton";
            this.runCycleButton.Size = new System.Drawing.Size(97, 23);
            this.runCycleButton.TabIndex = 1;
            this.runCycleButton.Text = "Run CPU Cycle";
            this.runCycleButton.UseVisualStyleBackColor = true;
            this.runCycleButton.Click += new System.EventHandler(this.OnRunCycleButtonClick);
            // 
            // manualControlsLabel
            // 
            this.manualControlsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.manualControlsLabel.Location = new System.Drawing.Point(3, 4);
            this.manualControlsLabel.Name = "manualControlsLabel";
            this.manualControlsLabel.Size = new System.Drawing.Size(208, 13);
            this.manualControlsLabel.TabIndex = 0;
            this.manualControlsLabel.Text = "Manual Module Controls";
            this.manualControlsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 462);
            this.Controls.Add(this.manualControlsPanel);
            this.Controls.Add(this.displayPanel);
            this.Controls.Add(this.memoryPanel);
            this.Controls.Add(this.cpuStatusPanel);
            this.Controls.Add(this.fileSelectPanel);
            this.Name = "MainWindow";
            this.Text = "Chip8 Interpreter";
            this.fileSelectPanel.ResumeLayout(false);
            this.fileSelectPanel.PerformLayout();
            this.cpuStatusPanel.ResumeLayout(false);
            this.nextInstructionPanel.ResumeLayout(false);
            this.nextInstructionPanel.PerformLayout();
            this.stackPanel.ResumeLayout(false);
            this.stackPanel.PerformLayout();
            this.specialRegistersPanel.ResumeLayout(false);
            this.specialRegistersPanel.PerformLayout();
            this.generalRegistersPanel.ResumeLayout(false);
            this.generalRegistersPanel.PerformLayout();
            this.memoryPanel.ResumeLayout(false);
            this.memoryViewport.ResumeLayout(false);
            this.memoryViewport.PerformLayout();
            this.displayPanel.ResumeLayout(false);
            this.manualControlsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Label openButtonLabel;
        private System.Windows.Forms.Panel fileSelectPanel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel cpuStatusPanel;
        private System.Windows.Forms.Label cpuStatusLabel;
        private System.Windows.Forms.Panel generalRegistersPanel;
        private System.Windows.Forms.Label generalRegistersLabel;
        private System.Windows.Forms.Panel specialRegistersPanel;
        private System.Windows.Forms.Label specialRegistersLabel;
        private System.Windows.Forms.Panel stackPanel;
        private System.Windows.Forms.Label stackLabel;
        private System.Windows.Forms.TextBox specialRegistersTextBox;
        private System.Windows.Forms.TextBox generalRegistersTextBox;
        private System.Windows.Forms.Panel memoryPanel;
        private System.Windows.Forms.TextBox stackTextBox;
        private System.Windows.Forms.Label memoryLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel memoryViewport;
        private System.Windows.Forms.TextBox memoryTextBox;
        private System.Windows.Forms.TextBox verticalHexTextBox;
        private System.Windows.Forms.Panel displayPanel;
        private System.Windows.Forms.Panel displayImagePanel;
        private System.Windows.Forms.Label displayLabel;
        private System.Windows.Forms.Panel manualControlsPanel;
        private System.Windows.Forms.Button runCycleButton;
        private System.Windows.Forms.Label manualControlsLabel;
        private System.Windows.Forms.Button updateDisplayButton;
        private System.Windows.Forms.Button decrementTimersButton;
        private System.Windows.Forms.Panel nextInstructionPanel;
        private System.Windows.Forms.Label nextInstructionLabel;
        private System.Windows.Forms.TextBox nextInstructionTextBox;
        private System.Windows.Forms.Label nextInstructionValueLabel;
    }
}

