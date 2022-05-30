using Deton.Fuels;

namespace Deton
{
    internal partial class DetonWiew : Form
    {
        public DetonWiew(IFuel[] fuels)
        {
            InitializeComponent();

            ACheckBox1.Checked = true;

            foreach (IFuel e in fuels)
            {
                initialFuelComboBox1.Items.Add(e);
                initialFuelComboBox2.Items.Add(e);
                initialFuelComboBox3.Items.Add(e);

                finalFuelComboBox1.Items.Add(e);
                finalFuelComboBox2.Items.Add(e);
                finalFuelComboBox3.Items.Add(e);
            }

            initialFuelComboBox1.SelectedIndex = 0;
            initialFuelComboBox2.SelectedIndex = 0;
            initialFuelComboBox3.SelectedIndex = 0;
            

            finalFuelComboBox1.SelectedIndex = 0;
            finalFuelComboBox2.SelectedIndex = 0;
            finalFuelComboBox3.SelectedIndex = 0;

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void CalculationButton_Click(object sender, EventArgs e)
        {
            if (ACheckBox1.Checked)
            {

            }
        }
    }
}