using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTD1
{
    public partial class SignalEditingControl : UserControl
    {
        public AnalogSignal signal;
        public SignalGraph signalGraph;
        public SignalEditingControl()
        {
            InitializeComponent();
            initialize();
        }
        private void initialize()
        {
            comboBox1.Items.Add("Sinusoida");
            comboBox1.Items.Add("Piłokształtny");
            comboBox1.Items.Add("Trójkątny");
            comboBox1.Items.Add("Prostokątny");
            comboBox1.Items.Add("Szum");
        }
        private void editSignal(int item, double amplitude, double frequency, double samplingFrequency, double length, int accuracyOfApproximation)
        {
            graphPanel.Controls.Remove(signalGraph);
            String label = "";
            signal = new AnalogSignal(amplitude, frequency, samplingFrequency, length);
            switch (item)
            {
                case 0:
                    label = "Sinusoida";
                    signal.GenerateSinusSignal();
                    break;
                case 1:
                    label = "Piłokształtny";
                    signal.GenerateSawSignal(accuracyOfApproximation);
                    break;
                case 2:
                    label = "Trójkątny";
                    signal.GenerateTriangleSignal(accuracyOfApproximation);
                    break;
                case 3:
                    label = "Prostokątny";
                    signal.GenerateSquareSignal(accuracyOfApproximation);
                    break;
                case 4:
                    label = "Szum";
                    signal.GenerateNoise(accuracyOfApproximation);
                    break;
            }
            signalGraph = new CourseGraph(3, 3, 150, 500, label);
            signalGraph.Signal = signal;
            signalGraph.Autoscale();
            signalGraph.DrawGraph();

            graphPanel.Controls.Add(signalGraph);
            
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {
                double amplitude = Convert.ToDouble(ampTextBox.Text);
                double frequency = Convert.ToDouble(freqTextBox.Text);
                double samplingFrequency = Convert.ToDouble(sampFreqTextBox.Text);
                double length = Convert.ToDouble(lengthTextBox.Text);
                int accuracyOfApproximation = Convert.ToInt16(accuracyTextBox.Text);
                editSignal(comboBox1.SelectedIndex, amplitude, frequency, samplingFrequency, length, accuracyOfApproximation);
            }
        }
    }
}
