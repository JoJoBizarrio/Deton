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

            initialFuelComboBox1.SelectedIndex = 1;
            initialFuelComboBox2.SelectedIndex = 0;
            initialFuelComboBox3.SelectedIndex = 0;

            finalFuelComboBox1.SelectedIndex = 1;
            finalFuelComboBox2.SelectedIndex = 0;
            finalFuelComboBox3.SelectedIndex = 0;

            InitialFuel1MolValueTextBoxA.Text = "1";
            FinalFuel1MolValueTextBoxA.Text = "1";

            InitialOxygenMolValueTextBoxA.Text = "1";
            FinalOxygenMolValueTextBoxA.Text = "2";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void CalculationButton_Click(object sender, EventArgs e)
        {

            if (ACheckBox1.Checked)
            {
                double[,] alfa = new double[1, 7];
                double[,] beta = new double[1, 7];

                alfa[0, 0] = Convert.ToDouble(InitialFuel1MolValueTextBoxA.Text);
                alfa[0, 1] = Convert.ToDouble(InitialFuel2MolValueTextBoxA.Text);
                alfa[0, 2] = Convert.ToDouble(InitialFuel3MolValueTextBoxA.Text);
                alfa[0, 3] = Convert.ToDouble(InitialOxygenMolValueTextBoxA.Text);
                alfa[0, 4] = Convert.ToDouble(InitialAirMolValueTextBoxA.Text);
                alfa[0, 5] = Convert.ToDouble(InitialArgonMolValueTextBoxA.Text);
                alfa[0, 6] = Convert.ToDouble(InitialNitrogenMolValueTextBoxA.Text);

                beta[0, 0] = Convert.ToDouble(FinalFuel1MolValueTextBoxA.Text);
                beta[0, 1] = Convert.ToDouble(FinalFuel2MolValueTextBoxA.Text);
                beta[0, 2] = Convert.ToDouble(FinalFuel3MolValueTextBoxA.Text);
                beta[0, 3] = Convert.ToDouble(FinalOxygenMolValueTextBoxA.Text);
                beta[0, 4] = Convert.ToDouble(FinalAirMolValueTextBoxA.Text);
                beta[0, 5] = Convert.ToDouble(FinalArgonMolValueTextBoxA.Text);
                beta[0, 6] = Convert.ToDouble(FinalNitrogenMolValueTextBoxA.Text);

                IFuel[] initialFuels = new IFuel[] { (IFuel)initialFuelComboBox1.SelectedItem, (IFuel)initialFuelComboBox2.SelectedItem, (IFuel)initialFuelComboBox3.SelectedItem };
                IFuel[] finalFuels = new IFuel[] { (IFuel)finalFuelComboBox1.SelectedItem, (IFuel)finalFuelComboBox2.SelectedItem, (IFuel)finalFuelComboBox3.SelectedItem };

                DetonLogic detonLogic = new DetonLogic();
                detonLogic.Detka(0, alfa, beta, initialFuels, finalFuels);
            }
        }
    }
}