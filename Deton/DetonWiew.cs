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

                alfaA[0] = Convert.ToDouble(InitialFuel1MolValueTextBoxA.Text);
                alfaA[1] = Convert.ToDouble(InitialFuel2MolValueTextBoxA.Text);
                alfaA[2] = Convert.ToDouble(InitialFuel3MolValueTextBoxA.Text);
                alfaA[3] = Convert.ToDouble(InitialOxygenMolValueTextBoxA.Text);
                alfaA[4] = Convert.ToDouble(InitialAirMolValueTextBoxA.Text);
                alfaA[5] = Convert.ToDouble(InitialArgonMolValueTextBoxA.Text);
                alfaA[6] = Convert.ToDouble(InitialNitrogenMolValueTextBoxA.Text);

                betaA[0] = Convert.ToDouble(FinalFuel1MolValueTextBoxA.Text);
                betaA[1] = Convert.ToDouble(FinalFuel2MolValueTextBoxA.Text);
                betaA[2] = Convert.ToDouble(FinalFuel3MolValueTextBoxA.Text);
                betaA[3] = Convert.ToDouble(FinalOxygenMolValueTextBoxA.Text);
                betaA[4] = Convert.ToDouble(FinalAirMolValueTextBoxA.Text);
                betaA[5] = Convert.ToDouble(FinalArgonMolValueTextBoxA.Text);
                betaA[6] = Convert.ToDouble(FinalNitrogenMolValueTextBoxA.Text);

                DetonationFunctions detonationFunctions = new DetonationFunctions(alfaA, betaA, initialFuels, finalFuels);
            }

            if (BCheckBox.Checked)
            {
                double[] alfaB = new double[7];
                double[] betaB = new double[7];

                double.TryParse(InitialFuel1MolValueTextBoxB.Text, out alfaB[0]);
                alfaB[0] = Convert.ToDouble(InitialFuel1MolValueTextBoxB.Text);
                alfaB[1] = Convert.ToDouble(InitialFuel2MolValueTextBoxB.Text);
                alfaB[2] = Convert.ToDouble(InitialFuel3MolValueTextBoxB.Text);
                alfaB[3] = Convert.ToDouble(InitialOxygenMolValueTextBoxB.Text);
                alfaB[4] = Convert.ToDouble(InitialAirMolValueTextBoxB.Text);
                alfaB[5] = Convert.ToDouble(InitialArgonMolValueTextBoxB.Text);
                alfaB[6] = Convert.ToDouble(InitialNitrogenMolValueTextBoxB.Text);

                betaB[0] = Convert.ToDouble(FinalFuel1MolValueTextBoxB.Text);
                betaB[1] = Convert.ToDouble(FinalFuel2MolValueTextBoxB.Text);
                betaB[2] = Convert.ToDouble(FinalFuel3MolValueTextBoxB.Text);
                betaB[3] = Convert.ToDouble(FinalOxygenMolValueTextBoxB.Text);
                betaB[4] = Convert.ToDouble(FinalAirMolValueTextBoxB.Text);
                betaB[5] = Convert.ToDouble(FinalArgonMolValueTextBoxB.Text);
                betaB[6] = Convert.ToDouble(FinalNitrogenMolValueTextBoxB.Text);

                DetonationFunctions BdetonationFunctions = new DetonationFunctions(alfaB, betaB, initialFuels, finalFuels);
            }

            if (CCheckBox.Checked)
            {
                double[] alfaC = new double[7];
                double[] betaC = new double[7];

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