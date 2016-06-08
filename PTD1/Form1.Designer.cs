namespace PTD1
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.wybierzLabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lab1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lab2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lab3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lab4FSKIPSKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lab5demodulacjaASKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lab5demodulacjaPSKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lab5demodulacjaFSKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aproksymacjaPrzebiegówToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mSEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zmianyMSEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autokorelacjaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.korelacjaWzajemnaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtracjaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stwórzNowyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtracjaDeltyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtracjaSzumuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wybierzLabToolStripMenuItem,
            this.stwórzNowyToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(788, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // wybierzLabToolStripMenuItem
            // 
            this.wybierzLabToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lab1ToolStripMenuItem,
            this.lab2ToolStripMenuItem,
            this.lab3ToolStripMenuItem,
            this.lab4FSKIPSKToolStripMenuItem,
            this.lab5demodulacjaASKToolStripMenuItem,
            this.lab5demodulacjaPSKToolStripMenuItem,
            this.lab5demodulacjaFSKToolStripMenuItem,
            this.aproksymacjaPrzebiegówToolStripMenuItem,
            this.mSEToolStripMenuItem,
            this.zmianyMSEToolStripMenuItem,
            this.autokorelacjaToolStripMenuItem,
            this.korelacjaWzajemnaToolStripMenuItem,
            this.filtracjaToolStripMenuItem,
            this.filtracjaDeltyToolStripMenuItem,
            this.filtracjaSzumuToolStripMenuItem});
            this.wybierzLabToolStripMenuItem.Name = "wybierzLabToolStripMenuItem";
            this.wybierzLabToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.wybierzLabToolStripMenuItem.Text = "Wybierz lab.";
            // 
            // lab1ToolStripMenuItem
            // 
            this.lab1ToolStripMenuItem.Name = "lab1ToolStripMenuItem";
            this.lab1ToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.lab1ToolStripMenuItem.Text = "lab1 i lab2";
            this.lab1ToolStripMenuItem.Click += new System.EventHandler(this.lab1Ilab2ToolStripMenuItem_Click);
            // 
            // lab2ToolStripMenuItem
            // 
            this.lab2ToolStripMenuItem.Name = "lab2ToolStripMenuItem";
            this.lab2ToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.lab2ToolStripMenuItem.Text = "lab3";
            this.lab2ToolStripMenuItem.Click += new System.EventHandler(this.lab3ToolStripMenuItem_Click);
            // 
            // lab3ToolStripMenuItem
            // 
            this.lab3ToolStripMenuItem.Name = "lab3ToolStripMenuItem";
            this.lab3ToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.lab3ToolStripMenuItem.Text = "lab4 (ASK)";
            this.lab3ToolStripMenuItem.Click += new System.EventHandler(this.lab4ASKToolStripMenuItem_Click);
            // 
            // lab4FSKIPSKToolStripMenuItem
            // 
            this.lab4FSKIPSKToolStripMenuItem.Name = "lab4FSKIPSKToolStripMenuItem";
            this.lab4FSKIPSKToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.lab4FSKIPSKToolStripMenuItem.Text = "lab4 (FSK i PSK)";
            this.lab4FSKIPSKToolStripMenuItem.Click += new System.EventHandler(this.lab4FSKIPSKToolStripMenuItem_Click);
            // 
            // lab5demodulacjaASKToolStripMenuItem
            // 
            this.lab5demodulacjaASKToolStripMenuItem.Name = "lab5demodulacjaASKToolStripMenuItem";
            this.lab5demodulacjaASKToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.lab5demodulacjaASKToolStripMenuItem.Text = "lab5 (demodulacja ASK)";
            this.lab5demodulacjaASKToolStripMenuItem.Click += new System.EventHandler(this.lab5demodulacjaASKToolStripMenuItem_Click);
            // 
            // lab5demodulacjaPSKToolStripMenuItem
            // 
            this.lab5demodulacjaPSKToolStripMenuItem.Name = "lab5demodulacjaPSKToolStripMenuItem";
            this.lab5demodulacjaPSKToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.lab5demodulacjaPSKToolStripMenuItem.Text = "lab5 (demodulacja PSK)";
            this.lab5demodulacjaPSKToolStripMenuItem.Click += new System.EventHandler(this.lab5demodulacjaPSKToolStripMenuItem_Click);
            // 
            // lab5demodulacjaFSKToolStripMenuItem
            // 
            this.lab5demodulacjaFSKToolStripMenuItem.Name = "lab5demodulacjaFSKToolStripMenuItem";
            this.lab5demodulacjaFSKToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.lab5demodulacjaFSKToolStripMenuItem.Text = "lab5 (demodulacja FSK)";
            this.lab5demodulacjaFSKToolStripMenuItem.Click += new System.EventHandler(this.lab5demodulacjaFSKToolStripMenuItem_Click);
            // 
            // aproksymacjaPrzebiegówToolStripMenuItem
            // 
            this.aproksymacjaPrzebiegówToolStripMenuItem.Name = "aproksymacjaPrzebiegówToolStripMenuItem";
            this.aproksymacjaPrzebiegówToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.aproksymacjaPrzebiegówToolStripMenuItem.Text = "Aproksymacja przebiegów";
            this.aproksymacjaPrzebiegówToolStripMenuItem.Click += new System.EventHandler(this.aproksymacjaPrzebiegówToolStripMenuItem_Click);
            // 
            // mSEToolStripMenuItem
            // 
            this.mSEToolStripMenuItem.Name = "mSEToolStripMenuItem";
            this.mSEToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.mSEToolStripMenuItem.Text = "MSE";
            this.mSEToolStripMenuItem.Click += new System.EventHandler(this.mSEToolStripMenuItem_Click);
            // 
            // zmianyMSEToolStripMenuItem
            // 
            this.zmianyMSEToolStripMenuItem.Name = "zmianyMSEToolStripMenuItem";
            this.zmianyMSEToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.zmianyMSEToolStripMenuItem.Text = "Zmiany MSE";
            this.zmianyMSEToolStripMenuItem.Click += new System.EventHandler(this.zmianyMSEToolStripMenuItem_Click);
            // 
            // autokorelacjaToolStripMenuItem
            // 
            this.autokorelacjaToolStripMenuItem.Name = "autokorelacjaToolStripMenuItem";
            this.autokorelacjaToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.autokorelacjaToolStripMenuItem.Text = "Autokorelacja";
            this.autokorelacjaToolStripMenuItem.Click += new System.EventHandler(this.autokorelacjaToolStripMenuItem_Click);
            // 
            // korelacjaWzajemnaToolStripMenuItem
            // 
            this.korelacjaWzajemnaToolStripMenuItem.Name = "korelacjaWzajemnaToolStripMenuItem";
            this.korelacjaWzajemnaToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.korelacjaWzajemnaToolStripMenuItem.Text = "Korelacja wzajemna";
            this.korelacjaWzajemnaToolStripMenuItem.Click += new System.EventHandler(this.korelacjaWzajemnaToolStripMenuItem_Click);
            // 
            // filtracjaToolStripMenuItem
            // 
            this.filtracjaToolStripMenuItem.Name = "filtracjaToolStripMenuItem";
            this.filtracjaToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.filtracjaToolStripMenuItem.Text = "Filtracja składowej";
            this.filtracjaToolStripMenuItem.Click += new System.EventHandler(this.filtracjaToolStripMenuItem_Click);
            // 
            // stwórzNowyToolStripMenuItem
            // 
            this.stwórzNowyToolStripMenuItem.Name = "stwórzNowyToolStripMenuItem";
            this.stwórzNowyToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.stwórzNowyToolStripMenuItem.Text = "Stwórz nowy";
            this.stwórzNowyToolStripMenuItem.Click += new System.EventHandler(this.stwórzNowyToolStripMenuItem_Click);
            // 
            // filtracjaDeltyToolStripMenuItem
            // 
            this.filtracjaDeltyToolStripMenuItem.Name = "filtracjaDeltyToolStripMenuItem";
            this.filtracjaDeltyToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.filtracjaDeltyToolStripMenuItem.Text = "Filtracja delty";
            this.filtracjaDeltyToolStripMenuItem.Click += new System.EventHandler(this.filtracjaDeltyToolStripMenuItem_Click);
            // 
            // filtracjaSzumuToolStripMenuItem
            // 
            this.filtracjaSzumuToolStripMenuItem.Name = "filtracjaSzumuToolStripMenuItem";
            this.filtracjaSzumuToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.filtracjaSzumuToolStripMenuItem.Text = "Filtracja szumu";
            this.filtracjaSzumuToolStripMenuItem.Click += new System.EventHandler(this.filtracjaSzumuToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 762);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem wybierzLabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lab1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lab2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lab3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lab4FSKIPSKToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lab5demodulacjaASKToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lab5demodulacjaPSKToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lab5demodulacjaFSKToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aproksymacjaPrzebiegówToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mSEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zmianyMSEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autokorelacjaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem korelacjaWzajemnaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stwórzNowyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtracjaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtracjaDeltyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtracjaSzumuToolStripMenuItem;







    }
}

