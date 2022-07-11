using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Deton.Fuels;
using Deton.Logic;

namespace Deton
{
    internal partial class DetonWiew : Form
    {
        public DetonWiew(IFuel[] fuels)
        {
            InitializeComponent();

            ACheckBox.Checked = true;

            foreach (IFuel e in fuels)
            {
                InitialFuelComboBox1.Items.Add(e);
                InitialFuelComboBox2.Items.Add(e);
                InitialFuelComboBox3.Items.Add(e);

                FinalFuelComboBox1.Items.Add(e);
                FinalFuelComboBox2.Items.Add(e);
                FinalFuelComboBox3.Items.Add(e);
            }

            InitialFuelComboBox1.SelectedIndex = 1;
            InitialFuelComboBox2.SelectedIndex = 0;
            InitialFuelComboBox3.SelectedIndex = 0;

            FinalFuelComboBox1.SelectedIndex = 1;
            FinalFuelComboBox2.SelectedIndex = 0;
            FinalFuelComboBox3.SelectedIndex = 0;

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
            if (!ACheckBox.Checked && !BCheckBox.Checked && !CCheckBox.Checked)
            {
                MessageBox.Show("Choose A, B or C.", "Initial parameters");
                return;
            }

            if (InitialFuelComboBox1.SelectedIndex == 0 && InitialFuelComboBox2.SelectedIndex == 0 && InitialFuelComboBox3.SelectedIndex == 0)
            {
                MessageBox.Show("Choose initial fuels", "Initial parameters");
                return;
            }

            if (FinalFuelComboBox1.SelectedIndex == 0 && FinalFuelComboBox2.SelectedIndex == 0 && FinalFuelComboBox3.SelectedIndex == 0)
            {
                MessageBox.Show("Choose final fuels", "Initial parameters");
                return;
            }

            IFuel[] initialFuels = new IFuel[] { (IFuel)InitialFuelComboBox1.SelectedItem, (IFuel)InitialFuelComboBox2.SelectedItem, (IFuel)InitialFuelComboBox3.SelectedItem };
            IFuel[] finalFuels = new IFuel[] { (IFuel)FinalFuelComboBox1.SelectedItem, (IFuel)FinalFuelComboBox2.SelectedItem, (IFuel)FinalFuelComboBox3.SelectedItem };

            if (ACheckBox.Checked)
            {
                double[] alfaA = new double[7];
                double[] betaA = new double[7];

                InitialFuel1MolValueTextBoxA.Text = InitialFuel1MolValueTextBoxA.Text.Replace('.', ',');
                InitialFuel2MolValueTextBoxA.Text = InitialFuel2MolValueTextBoxA.Text.Replace('.', ',');
                InitialFuel3MolValueTextBoxA.Text = InitialFuel3MolValueTextBoxA.Text.Replace('.', ',');
                InitialOxygenMolValueTextBoxA.Text = InitialOxygenMolValueTextBoxA.Text.Replace('.', ',');
                InitialAirMolValueTextBoxA.Text = InitialAirMolValueTextBoxA.Text.Replace('.', ',');
                InitialArgonMolValueTextBoxA.Text = InitialArgonMolValueTextBoxA.Text.Replace('.', ',');
                InitialNitrogenMolValueTextBoxA.Text = InitialNitrogenMolValueTextBoxA.Text.Replace('.', ',');

                FinalFuel1MolValueTextBoxA.Text = FinalFuel1MolValueTextBoxA.Text.Replace('.', ',');
                FinalFuel2MolValueTextBoxA.Text = FinalFuel2MolValueTextBoxA.Text.Replace('.', ',');
                FinalFuel3MolValueTextBoxA.Text = FinalFuel3MolValueTextBoxA.Text.Replace('.', ',');
                FinalOxygenMolValueTextBoxA.Text = FinalOxygenMolValueTextBoxA.Text.Replace('.', ',');
                FinalAirMolValueTextBoxA.Text = FinalAirMolValueTextBoxA.Text.Replace('.', ',');
                FinalArgonMolValueTextBoxA.Text = FinalArgonMolValueTextBoxA.Text.Replace('.', ',');
                FinalNitrogenMolValueTextBoxA.Text = FinalNitrogenMolValueTextBoxA.Text.Replace('.', ',');

                if (!double.TryParse(InitialFuel1MolValueTextBoxA.Text, out alfaA[0]) ||
                    !double.TryParse(InitialFuel2MolValueTextBoxA.Text, out alfaA[1]) ||
                    !double.TryParse(InitialFuel3MolValueTextBoxA.Text, out alfaA[2]) ||
                    !double.TryParse(InitialOxygenMolValueTextBoxA.Text, out alfaA[3]) ||
                    !double.TryParse(InitialAirMolValueTextBoxA.Text, out alfaA[4]) ||
                    !double.TryParse(InitialArgonMolValueTextBoxA.Text, out alfaA[5]) ||
                    !double.TryParse(InitialNitrogenMolValueTextBoxA.Text, out alfaA[6]))
                {
                    MessageBox.Show("Incorrect entered value(s) of initial mix.", "Initial parameters");
                    return;
                }

                if (!double.TryParse(FinalFuel1MolValueTextBoxA.Text, out betaA[0]) ||
                    !double.TryParse(FinalFuel2MolValueTextBoxA.Text, out betaA[1]) ||
                    !double.TryParse(FinalFuel3MolValueTextBoxA.Text, out betaA[2]) ||
                    !double.TryParse(FinalOxygenMolValueTextBoxA.Text, out betaA[3]) ||
                    !double.TryParse(FinalAirMolValueTextBoxA.Text, out betaA[4]) ||
                    !double.TryParse(FinalArgonMolValueTextBoxA.Text, out betaA[5]) ||
                    !double.TryParse(FinalNitrogenMolValueTextBoxA.Text, out betaA[6]))
                {
                    MessageBox.Show("Incorrect entered value(s) of final mix.", "Initial parameters");
                    return;
                }

                DetonationFunctions detonationFunctions = new DetonationFunctions(alfaA, betaA, initialFuels, finalFuels);
            }

            if (BCheckBox.Checked)
            {
                double[] alfaB = new double[7];
                double[] betaB = new double[7];

                InitialFuel1MolValueTextBoxB.Text = InitialFuel1MolValueTextBoxB.Text.Replace('.', ',');
                InitialFuel2MolValueTextBoxB.Text = InitialFuel2MolValueTextBoxB.Text.Replace('.', ',');
                InitialFuel3MolValueTextBoxB.Text = InitialFuel3MolValueTextBoxB.Text.Replace('.', ',');
                InitialOxygenMolValueTextBoxB.Text = InitialOxygenMolValueTextBoxB.Text.Replace('.', ',');
                InitialAirMolValueTextBoxB.Text = InitialAirMolValueTextBoxB.Text.Replace('.', ',');
                InitialArgonMolValueTextBoxB.Text = InitialArgonMolValueTextBoxB.Text.Replace('.', ',');
                InitialNitrogenMolValueTextBoxB.Text = InitialNitrogenMolValueTextBoxB.Text.Replace('.', ',');

                FinalFuel1MolValueTextBoxB.Text = FinalFuel1MolValueTextBoxB.Text.Replace('.', ',');
                FinalFuel2MolValueTextBoxB.Text = FinalFuel2MolValueTextBoxB.Text.Replace('.', ',');
                FinalFuel3MolValueTextBoxB.Text = FinalFuel3MolValueTextBoxB.Text.Replace('.', ',');
                FinalOxygenMolValueTextBoxB.Text = FinalOxygenMolValueTextBoxB.Text.Replace('.', ',');
                FinalAirMolValueTextBoxB.Text = FinalAirMolValueTextBoxB.Text.Replace('.', ',');
                FinalArgonMolValueTextBoxB.Text = FinalArgonMolValueTextBoxB.Text.Replace('.', ',');
                FinalNitrogenMolValueTextBoxB.Text = FinalNitrogenMolValueTextBoxB.Text.Replace('.', ',');

                if (!double.TryParse(InitialFuel1MolValueTextBoxB.Text, out alfaB[0]) ||
                    !double.TryParse(InitialFuel2MolValueTextBoxB.Text, out alfaB[1]) ||
                    !double.TryParse(InitialFuel3MolValueTextBoxB.Text, out alfaB[2]) ||
                    !double.TryParse(InitialOxygenMolValueTextBoxB.Text, out alfaB[3]) ||
                    !double.TryParse(InitialAirMolValueTextBoxB.Text, out alfaB[4]) ||
                    !double.TryParse(InitialArgonMolValueTextBoxB.Text, out alfaB[5]) ||
                    !double.TryParse(InitialNitrogenMolValueTextBoxB.Text, out alfaB[6]))
                {
                    MessageBox.Show("Incorrect entered value(s) of initial mix.", "Initial parameters");
                    return;
                }

                if (!double.TryParse(FinalFuel1MolValueTextBoxB.Text, out betaB[0]) ||
                    !double.TryParse(FinalFuel2MolValueTextBoxB.Text, out betaB[1]) ||
                    !double.TryParse(FinalFuel3MolValueTextBoxB.Text, out betaB[2]) ||
                    !double.TryParse(FinalOxygenMolValueTextBoxB.Text, out betaB[3]) ||
                    !double.TryParse(FinalAirMolValueTextBoxB.Text, out betaB[4]) ||
                    !double.TryParse(FinalArgonMolValueTextBoxB.Text, out betaB[5]) ||
                    !double.TryParse(FinalNitrogenMolValueTextBoxB.Text, out betaB[6]))
                {
                    MessageBox.Show("Incorrect entered value(s) of final mix.", "Initial parameters");
                    return;
                }

                //alfaB[0] = Convert.ToDouble(InitialFuel1MolValueTextBoxB.Text);
                //alfaB[1] = Convert.ToDouble(InitialFuel2MolValueTextBoxB.Text);
                //alfaB[2] = Convert.ToDouble(InitialFuel3MolValueTextBoxB.Text);
                //alfaB[3] = Convert.ToDouble(InitialOxygenMolValueTextBoxB.Text);
                //alfaB[4] = Convert.ToDouble(InitialAirMolValueTextBoxB.Text);
                //alfaB[5] = Convert.ToDouble(InitialArgonMolValueTextBoxB.Text);
                //alfaB[6] = Convert.ToDouble(InitialNitrogenMolValueTextBoxB.Text);

                //betaB[0] = Convert.ToDouble(FinalFuel1MolValueTextBoxB.Text);
                //betaB[1] = Convert.ToDouble(FinalFuel2MolValueTextBoxB.Text);
                //betaB[2] = Convert.ToDouble(FinalFuel3MolValueTextBoxB.Text);
                //betaB[3] = Convert.ToDouble(FinalOxygenMolValueTextBoxB.Text);
                //betaB[4] = Convert.ToDouble(FinalAirMolValueTextBoxB.Text);
                //betaB[5] = Convert.ToDouble(FinalArgonMolValueTextBoxB.Text);
                //betaB[6] = Convert.ToDouble(FinalNitrogenMolValueTextBoxB.Text);

                DetonationFunctions BdetonationFunctions = new DetonationFunctions(alfaB, betaB, initialFuels, finalFuels);
            }

            if (CCheckBox.Checked)
            {
                double[] alfaC = new double[7];
                double[] betaC = new double[7];

                InitialFuel1MolValueTextBoxC.Text = InitialFuel1MolValueTextBoxC.Text.Replace('.', ',');
                InitialFuel2MolValueTextBoxC.Text = InitialFuel2MolValueTextBoxC.Text.Replace('.', ',');
                InitialFuel3MolValueTextBoxC.Text = InitialFuel3MolValueTextBoxC.Text.Replace('.', ',');
                InitialOxygenMolValueTextBoxC.Text = InitialOxygenMolValueTextBoxC.Text.Replace('.', ',');
                InitialAirMolValueTextBoxC.Text = InitialAirMolValueTextBoxC.Text.Replace('.', ',');
                InitialArgonMolValueTextBoxC.Text = InitialArgonMolValueTextBoxC.Text.Replace('.', ',');
                InitialNitrogenMolValueTextBoxC.Text = InitialNitrogenMolValueTextBoxC.Text.Replace('.', ',');

                FinalFuel1MolValueTextBoxC.Text = FinalFuel1MolValueTextBoxC.Text.Replace('.', ',');
                FinalFuel2MolValueTextBoxC.Text = FinalFuel2MolValueTextBoxC.Text.Replace('.', ',');
                FinalFuel3MolValueTextBoxC.Text = FinalFuel3MolValueTextBoxC.Text.Replace('.', ',');
                FinalOxygenMolValueTextBoxC.Text = FinalOxygenMolValueTextBoxC.Text.Replace('.', ',');
                FinalAirMolValueTextBoxC.Text = FinalAirMolValueTextBoxC.Text.Replace('.', ',');
                FinalArgonMolValueTextBoxC.Text = FinalArgonMolValueTextBoxC.Text.Replace('.', ',');
                FinalNitrogenMolValueTextBoxC.Text = FinalNitrogenMolValueTextBoxC.Text.Replace('.', ',');

                alfaC[0] = Convert.ToDouble(InitialFuel1MolValueTextBoxC.Text);
                alfaC[1] = Convert.ToDouble(InitialFuel2MolValueTextBoxC.Text);
                alfaC[2] = Convert.ToDouble(InitialFuel3MolValueTextBoxC.Text);
                alfaC[3] = Convert.ToDouble(InitialOxygenMolValueTextBoxC.Text);
                alfaC[4] = Convert.ToDouble(InitialAirMolValueTextBoxC.Text);
                alfaC[5] = Convert.ToDouble(InitialArgonMolValueTextBoxC.Text);
                alfaC[6] = Convert.ToDouble(InitialNitrogenMolValueTextBoxC.Text);

                betaC[0] = Convert.ToDouble(FinalFuel1MolValueTextBoxC.Text);
                betaC[1] = Convert.ToDouble(FinalFuel2MolValueTextBoxC.Text);
                betaC[2] = Convert.ToDouble(FinalFuel3MolValueTextBoxC.Text);
                betaC[3] = Convert.ToDouble(FinalOxygenMolValueTextBoxC.Text);
                betaC[4] = Convert.ToDouble(FinalAirMolValueTextBoxC.Text);
                betaC[5] = Convert.ToDouble(FinalArgonMolValueTextBoxC.Text);
                betaC[6] = Convert.ToDouble(FinalNitrogenMolValueTextBoxC.Text);

                DetonationFunctions CdetonationFunctions = new DetonationFunctions(alfaC, betaC, initialFuels, finalFuels);
            }

            MessageBox.Show("Done.", "Calculations completed");
        }
    }
}