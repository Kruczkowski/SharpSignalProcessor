namespace PTD1
{
    partial class SignalEditingControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.ampLabel = new System.Windows.Forms.Label();
            this.freqLabel = new System.Windows.Forms.Label();
            this.samplingFreqLabel = new System.Windows.Forms.Label();
            this.lengthLabel = new System.Windows.Forms.Label();
            this.ampTextBox = new System.Windows.Forms.TextBox();
            this.lengthTextBox = new System.Windows.Forms.TextBox();
            this.sampFreqTextBox = new System.Windows.Forms.TextBox();
            this.freqTextBox = new System.Windows.Forms.TextBox();
            this.acceptButton = new System.Windows.Forms.Button();
            this.accuracyTextBox = new System.Windows.Forms.TextBox();
            this.accuracyLabel = new System.Windows.Forms.Label();
            this.signalPanel = new System.Windows.Forms.Panel();
            this.graphPanel = new System.Windows.Forms.Panel();
            this.removeButton = new System.Windows.Forms.Button();
            this.signalPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // ampLabel
            // 
            this.ampLabel.AutoSize = true;
            this.ampLabel.Location = new System.Drawing.Point(2, 31);
            this.ampLabel.Name = "ampLabel";
            this.ampLabel.Size = new System.Drawing.Size(53, 13);
            this.ampLabel.TabIndex = 2;
            this.ampLabel.Text = "Amplituda";
            // 
            // freqLabel
            // 
            this.freqLabel.AutoSize = true;
            this.freqLabel.Location = new System.Drawing.Point(2, 57);
            this.freqLabel.Name = "freqLabel";
            this.freqLabel.Size = new System.Drawing.Size(71, 13);
            this.freqLabel.TabIndex = 3;
            this.freqLabel.Text = "Częstotliwość";
            // 
            // samplingFreqLabel
            // 
            this.samplingFreqLabel.AutoSize = true;
            this.samplingFreqLabel.Location = new System.Drawing.Point(2, 83);
            this.samplingFreqLabel.Name = "samplingFreqLabel";
            this.samplingFreqLabel.Size = new System.Drawing.Size(86, 13);
            this.samplingFreqLabel.TabIndex = 4;
            this.samplingFreqLabel.Text = "Cz. próbkowania";
            // 
            // lengthLabel
            // 
            this.lengthLabel.AutoSize = true;
            this.lengthLabel.Location = new System.Drawing.Point(2, 112);
            this.lengthLabel.Name = "lengthLabel";
            this.lengthLabel.Size = new System.Drawing.Size(78, 13);
            this.lengthLabel.TabIndex = 5;
            this.lengthLabel.Text = "Rozmiar bufora";
            // 
            // ampTextBox
            // 
            this.ampTextBox.Location = new System.Drawing.Point(88, 31);
            this.ampTextBox.Name = "ampTextBox";
            this.ampTextBox.Size = new System.Drawing.Size(100, 20);
            this.ampTextBox.TabIndex = 6;
            this.ampTextBox.Text = "1";
            // 
            // lengthTextBox
            // 
            this.lengthTextBox.Location = new System.Drawing.Point(88, 109);
            this.lengthTextBox.Name = "lengthTextBox";
            this.lengthTextBox.Size = new System.Drawing.Size(100, 20);
            this.lengthTextBox.TabIndex = 7;
            this.lengthTextBox.Text = "0,5";
            // 
            // sampFreqTextBox
            // 
            this.sampFreqTextBox.Location = new System.Drawing.Point(88, 83);
            this.sampFreqTextBox.Name = "sampFreqTextBox";
            this.sampFreqTextBox.Size = new System.Drawing.Size(100, 20);
            this.sampFreqTextBox.TabIndex = 8;
            this.sampFreqTextBox.Text = "1000";
            // 
            // freqTextBox
            // 
            this.freqTextBox.Location = new System.Drawing.Point(88, 57);
            this.freqTextBox.Name = "freqTextBox";
            this.freqTextBox.Size = new System.Drawing.Size(100, 20);
            this.freqTextBox.TabIndex = 9;
            this.freqTextBox.Text = "100";
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(113, 161);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(75, 23);
            this.acceptButton.TabIndex = 10;
            this.acceptButton.Text = "Akceptuj";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // accuracyTextBox
            // 
            this.accuracyTextBox.Location = new System.Drawing.Point(88, 135);
            this.accuracyTextBox.Name = "accuracyTextBox";
            this.accuracyTextBox.Size = new System.Drawing.Size(100, 20);
            this.accuracyTextBox.TabIndex = 12;
            this.accuracyTextBox.Text = "10";
            // 
            // accuracyLabel
            // 
            this.accuracyLabel.AutoSize = true;
            this.accuracyLabel.Location = new System.Drawing.Point(2, 138);
            this.accuracyLabel.Name = "accuracyLabel";
            this.accuracyLabel.Size = new System.Drawing.Size(66, 13);
            this.accuracyLabel.TabIndex = 11;
            this.accuracyLabel.Text = "Dokładność";
            // 
            // signalPanel
            // 
            this.signalPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.signalPanel.Controls.Add(this.comboBox1);
            this.signalPanel.Controls.Add(this.accuracyTextBox);
            this.signalPanel.Controls.Add(this.ampLabel);
            this.signalPanel.Controls.Add(this.accuracyLabel);
            this.signalPanel.Controls.Add(this.freqLabel);
            this.signalPanel.Controls.Add(this.acceptButton);
            this.signalPanel.Controls.Add(this.samplingFreqLabel);
            this.signalPanel.Controls.Add(this.freqTextBox);
            this.signalPanel.Controls.Add(this.lengthLabel);
            this.signalPanel.Controls.Add(this.sampFreqTextBox);
            this.signalPanel.Controls.Add(this.ampTextBox);
            this.signalPanel.Controls.Add(this.lengthTextBox);
            this.signalPanel.Location = new System.Drawing.Point(3, 3);
            this.signalPanel.Name = "signalPanel";
            this.signalPanel.Size = new System.Drawing.Size(210, 200);
            this.signalPanel.TabIndex = 13;
            this.signalPanel.Tag = "Sygnał";
            // 
            // graphPanel
            // 
            this.graphPanel.AllowDrop = true;
            this.graphPanel.BackColor = System.Drawing.SystemColors.Window;
            this.graphPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphPanel.Location = new System.Drawing.Point(220, 3);
            this.graphPanel.Name = "graphPanel";
            this.graphPanel.Size = new System.Drawing.Size(506, 200);
            this.graphPanel.TabIndex = 14;
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(651, 209);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 15;
            this.removeButton.Text = "Usuń";
            this.removeButton.UseVisualStyleBackColor = true;
            // 
            // SignalEditingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.graphPanel);
            this.Controls.Add(this.signalPanel);
            this.Name = "SignalEditingControl";
            this.Size = new System.Drawing.Size(731, 236);
            this.signalPanel.ResumeLayout(false);
            this.signalPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label ampLabel;
        private System.Windows.Forms.Label freqLabel;
        private System.Windows.Forms.Label samplingFreqLabel;
        private System.Windows.Forms.Label lengthLabel;
        private System.Windows.Forms.TextBox ampTextBox;
        private System.Windows.Forms.TextBox lengthTextBox;
        private System.Windows.Forms.TextBox sampFreqTextBox;
        private System.Windows.Forms.TextBox freqTextBox;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.TextBox accuracyTextBox;
        private System.Windows.Forms.Label accuracyLabel;
        private System.Windows.Forms.Panel signalPanel;
        private System.Windows.Forms.Panel graphPanel;
        public System.Windows.Forms.Button removeButton;

    }
}
