using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTD1
{
    public partial class EditingWindow : Form
    {
        public EditingWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Zdarzenie przycisku dodaj
        /// </summary>
        private void dodajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SignalEditingControl control = new SignalEditingControl();
            control.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            panel1.Controls.Add(control);
        }

        /// <summary>
        /// Zdarzenie zmiany rozmiaru okna
        /// </summary>
        private void EditingWindow_ResizeEnd(object sender, EventArgs e)
        {
            panel1.Size = this.Size;
        }

        /// <summary>
        /// Zdarzenie wciśnięcia przycisku removeButton
        /// </summary>
        private void removeButton_Click(object sender, EventArgs e)
        {
            foreach (Control control in panel1.Controls)
            {
                if (sender == ((SignalEditingControl)control).removeButton)
                {
                    panel1.Controls.Remove(control);
                    break;
                }
            }
            this.Refresh();
        }
    }
}
