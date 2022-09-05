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

        private void UpdateO2()
        {
            

            Mixture mixture = new Mixture((IFuel)InitialFuelComboBox1.SelectedItem, (IFuel)InitialFuelComboBox2.SelectedItem, (IFuel)InitialFuelComboBox3.SelectedItem,
                                           Convert.ToDouble(InitialFuel1MolValueTextBoxA.Text), Convert.ToDouble(InitialFuel2MolValueTextBoxA.Text),
                                           Convert.ToDouble(InitialFuel3MolValueTextBoxA.Text), Convert.ToDouble(InitialOxygenMolValueTextBoxA.Text),
                                           Convert.ToDouble(InitialAirMolValueTextBoxA.Text), Convert.ToDouble(InitialNitrogenMolValueTextBoxA.Text),
                                           Convert.ToDouble(InitialArgonMolValueTextBoxA.Text));

            InitialEquimolarTextBoxA.Text = Convert.ToString(mixture.O2toEquimolarO2Value);
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

            //Mixture initialMixture = new Mixture((IFuel)InitialFuelComboBox1.SelectedItem, (IFuel)InitialFuelComboBox2.SelectedItem, (IFuel)InitialFuelComboBox3.SelectedItem, InitialFuel1MolValueTextBoxA.Text);
            IFuel[] initialFuels = new IFuel[] { (IFuel)InitialFuelComboBox1.SelectedItem, (IFuel)InitialFuelComboBox2.SelectedItem, (IFuel)InitialFuelComboBox3.SelectedItem };
            IFuel[] finalFuels = new IFuel[] { (IFuel)FinalFuelComboBox1.SelectedItem, (IFuel)FinalFuelComboBox2.SelectedItem, (IFuel)FinalFuelComboBox3.SelectedItem };

            if (ACheckBox.Checked)
            {
                double[] alfaA = new double[7];
                double[] betaA = new double[7];

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

        private void InitialFuel1MolValueTextBoxA_KeyPress(object sender, KeyPressEventArgs e)
        {
            char symbol = e.KeyChar;

            if (!Char.IsDigit(symbol) && symbol != 8 && symbol != 44)
            {
                e.Handled = true;
            }


            UpdateO2();
        }

        private void UpdateO2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mixture mixture = new Mixture((IFuel)InitialFuelComboBox1.SelectedItem, (IFuel)InitialFuelComboBox2.SelectedItem, (IFuel)InitialFuelComboBox3.SelectedItem,
                                          Convert.ToDouble(InitialFuel1MolValueTextBoxA.Text), Convert.ToDouble(InitialFuel2MolValueTextBoxA.Text),
                                          Convert.ToDouble(InitialFuel3MolValueTextBoxA.Text), Convert.ToDouble(InitialOxygenMolValueTextBoxA.Text),
                                          Convert.ToDouble(InitialAirMolValueTextBoxA.Text), Convert.ToDouble(InitialNitrogenMolValueTextBoxA.Text),
                                          Convert.ToDouble(InitialArgonMolValueTextBoxA.Text));

            InitialEquimolarTextBoxA.Text = Convert.ToString(mixture.O2toEquimolarO2Value);
        }

        private void ExitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}