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

namespace Deton.Graphically
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

        private void ExitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void MixtureKeySeparatorA_KeyPress(object sender, KeyPressEventArgs e)
        {
            char symbol = e.KeyChar;

            if (!Char.IsDigit(symbol) && symbol != 8 && symbol != 44)
            {
                e.Handled = true;
            }
        }

        private void UpdateO2_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = new TextBox();

            if (sender.GetType() == textBox.GetType())
            {
                textBox = sender as TextBox;

                if (string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.AppendText("0");
                    textBox.SelectAll();
                }
            }

            if (double.TryParse(InitialFuel1MolValueTextBoxA.Text, out double InitialFuel1MolValueA) && double.TryParse(InitialFuel2MolValueTextBoxA.Text, out double InitialFuel2MolValueA) &&
            double.TryParse(InitialFuel3MolValueTextBoxA.Text, out double InitialFuel3MolValueA) && double.TryParse(InitialOxygenMolValueTextBoxA.Text, out double InitialOxygenMolValueA) &&
            double.TryParse(InitialAirMolValueTextBoxA.Text, out double InitialAirMolValueA) && double.TryParse(InitialNitrogenMolValueTextBoxA.Text, out double InitialNitrogenMolValueA) &&
            double.TryParse(InitialArgonMolValueTextBoxA.Text, out double InitialArgonMolValueA))
            {
                Mixture initialMixtureA = new Mixture((IFuel)InitialFuelComboBox1.SelectedItem, (IFuel)InitialFuelComboBox2.SelectedItem, (IFuel)InitialFuelComboBox3.SelectedItem,
                                                      InitialFuel1MolValueA, InitialFuel2MolValueA, InitialFuel3MolValueA,
                                                      InitialOxygenMolValueA, InitialAirMolValueA, InitialNitrogenMolValueA, InitialArgonMolValueA);

                InitialEquimolarTextBoxA.Text = Convert.ToString(Math.Round(initialMixtureA.O2toEquimolarO2Value, 2, MidpointRounding.AwayFromZero));
                InitialStoichiometricTextBoxA.Text = Convert.ToString(Math.Round(initialMixtureA.O2toStoichiometricO2Value, 2, MidpointRounding.AwayFromZero));
            }
            else
            {
                InitialEquimolarTextBoxA.Text = "������";
                InitialStoichiometricTextBoxA.Text = "������";
            }

            if (double.TryParse(FinalFuel1MolValueTextBoxA.Text, out double FinalFuel1MolValueA) && double.TryParse(FinalFuel2MolValueTextBoxA.Text, out double FinalFuel2MolValueA) &&
            double.TryParse(FinalFuel3MolValueTextBoxA.Text, out double FinalFuel3MolValueA) && double.TryParse(FinalOxygenMolValueTextBoxA.Text, out double FinalOxygenMolValueA) &&
            double.TryParse(FinalAirMolValueTextBoxA.Text, out double FinalAirMolValueA) && double.TryParse(FinalNitrogenMolValueTextBoxA.Text, out double FinalNitrogenMolValueA) &&
            double.TryParse(FinalArgonMolValueTextBoxA.Text, out double FinalArgonMolValueA))
            {
                Mixture finalMixtureA = new Mixture((IFuel)FinalFuelComboBox1.SelectedItem, (IFuel)FinalFuelComboBox2.SelectedItem, (IFuel)FinalFuelComboBox3.SelectedItem,
                                                     FinalFuel1MolValueA, FinalFuel2MolValueA, FinalFuel3MolValueA,
                                                     FinalOxygenMolValueA, FinalAirMolValueA, FinalNitrogenMolValueA, FinalArgonMolValueA);

                FinalEquimolarTextBoxA.Text = Convert.ToString(Math.Round(finalMixtureA.O2toEquimolarO2Value, 2, MidpointRounding.AwayFromZero));
                FinalStoichiometricTextBoxA.Text = Convert.ToString(Math.Round(finalMixtureA.O2toStoichiometricO2Value, 2, MidpointRounding.AwayFromZero));
            }
            else
            {
                InitialEquimolarTextBoxA.Text = "������";
                InitialStoichiometricTextBoxA.Text = "������";
            }

            if (double.TryParse(InitialFuel1MolValueTextBoxB.Text, out double InitialFuel1MolValueB) && double.TryParse(InitialFuel2MolValueTextBoxB.Text, out double InitialFuel2MolValueB) &&
            double.TryParse(InitialFuel3MolValueTextBoxB.Text, out double InitialFuel3MolValueB) && double.TryParse(InitialOxygenMolValueTextBoxB.Text, out double InitialOxygenMolValueB) &&
            double.TryParse(InitialAirMolValueTextBoxB.Text, out double InitialAirMolValueB) && double.TryParse(InitialNitrogenMolValueTextBoxB.Text, out double InitialNitrogenMolValueB) &&
            double.TryParse(InitialArgonMolValueTextBoxB.Text, out double InitialArgonMolValueB))
            {
                Mixture initialMixtureB = new Mixture((IFuel)InitialFuelComboBox1.SelectedItem, (IFuel)InitialFuelComboBox2.SelectedItem, (IFuel)InitialFuelComboBox3.SelectedItem,
                                                         InitialFuel1MolValueB, InitialFuel2MolValueB, InitialFuel3MolValueB,
                                                         InitialOxygenMolValueB, InitialAirMolValueB, InitialNitrogenMolValueB, InitialArgonMolValueB);

                InitialEquimolarTextBoxB.Text = Convert.ToString(Math.Round(initialMixtureB.O2toEquimolarO2Value, 2, MidpointRounding.AwayFromZero));
                InitialStoichiometricTextBoxB.Text = Convert.ToString(Math.Round(initialMixtureB.O2toStoichiometricO2Value, 2, MidpointRounding.AwayFromZero));
            }
            else
            {
                InitialEquimolarTextBoxB.Text = "������";
                InitialStoichiometricTextBoxB.Text = "������";
            }

            if (double.TryParse(FinalFuel1MolValueTextBoxB.Text, out double FinalFuel1MolValueB) && double.TryParse(FinalFuel2MolValueTextBoxB.Text, out double FinalFuel2MolValueB) &&
             double.TryParse(FinalFuel3MolValueTextBoxB.Text, out double FinalFuel3MolValueB) && double.TryParse(FinalOxygenMolValueTextBoxB.Text, out double FinalOxygenMolValueB) &&
             double.TryParse(FinalAirMolValueTextBoxB.Text, out double FinalAirMolValueB) && double.TryParse(FinalNitrogenMolValueTextBoxB.Text, out double FinalNitrogenMolValueB) &&
             double.TryParse(FinalArgonMolValueTextBoxB.Text, out double FinalArgonMolValueB))
            {


                Mixture finalMixtureB = new Mixture((IFuel)FinalFuelComboBox1.SelectedItem, (IFuel)FinalFuelComboBox2.SelectedItem, (IFuel)FinalFuelComboBox3.SelectedItem,
                                                     FinalFuel1MolValueB, FinalFuel2MolValueB, FinalFuel3MolValueB,
                                                     FinalOxygenMolValueB, FinalAirMolValueB, FinalNitrogenMolValueB, FinalArgonMolValueB);

                FinalEquimolarTextBoxB.Text = Convert.ToString(Math.Round(finalMixtureB.O2toEquimolarO2Value, 2, MidpointRounding.AwayFromZero));
                FinalStoichiometricTextBoxB.Text = Convert.ToString(Math.Round(finalMixtureB.O2toStoichiometricO2Value, 2, MidpointRounding.AwayFromZero));
            }
            else
            {
                FinalEquimolarTextBoxB.Text = "������";
                FinalStoichiometricTextBoxB.Text = "������";
            }


            if (double.TryParse(InitialFuel1MolValueTextBoxC.Text, out double InitialFuel1MolValueC) && double.TryParse(InitialFuel2MolValueTextBoxC.Text, out double InitialFuel2MolValueC) &&
            double.TryParse(InitialFuel3MolValueTextBoxC.Text, out double InitialFuel3MolValueC) && double.TryParse(InitialOxygenMolValueTextBoxC.Text, out double InitialOxygenMolValueC) &&
            double.TryParse(InitialAirMolValueTextBoxC.Text, out double InitialAirMolValueC) && double.TryParse(InitialNitrogenMolValueTextBoxC.Text, out double InitialNitrogenMolValueC) &&
            double.TryParse(InitialArgonMolValueTextBoxC.Text, out double InitialArgonMolValueC))
            {
                Mixture initialMixtureC = new Mixture((IFuel)InitialFuelComboBox1.SelectedItem, (IFuel)InitialFuelComboBox2.SelectedItem, (IFuel)InitialFuelComboBox3.SelectedItem,
                                       InitialFuel1MolValueC, InitialFuel2MolValueC, InitialFuel3MolValueC,
                                       InitialOxygenMolValueC, InitialAirMolValueC, InitialNitrogenMolValueC, InitialArgonMolValueC);

                InitialEquimolarTextBoxC.Text = Convert.ToString(Math.Round(initialMixtureC.O2toEquimolarO2Value, 2, MidpointRounding.AwayFromZero));
                InitialStoichiometricTextBoxC.Text = Convert.ToString(Math.Round(initialMixtureC.O2toStoichiometricO2Value, 2, MidpointRounding.AwayFromZero));
            }
            else
            {
                InitialEquimolarTextBoxC.Text = "������";
                InitialStoichiometricTextBoxC.Text = "������";
            }

            if (double.TryParse(FinalFuel1MolValueTextBoxC.Text, out double FinalFuel1MolValueC) && double.TryParse(FinalFuel2MolValueTextBoxC.Text, out double FinalFuel2MolValueC) &&
            double.TryParse(FinalFuel3MolValueTextBoxC.Text, out double FinalFuel3MolValueC) && double.TryParse(FinalOxygenMolValueTextBoxC.Text, out double FinalOxygenMolValueC) &&
            double.TryParse(FinalAirMolValueTextBoxC.Text, out double FinalAirMolValueC) && double.TryParse(FinalNitrogenMolValueTextBoxC.Text, out double FinalNitrogenMolValueC) &&
            double.TryParse(FinalArgonMolValueTextBoxC.Text, out double FinalArgonMolValueC))
            {
                Mixture finalMixtureC = new Mixture((IFuel)FinalFuelComboBox1.SelectedItem, (IFuel)FinalFuelComboBox2.SelectedItem, (IFuel)FinalFuelComboBox3.SelectedItem,
                                                     FinalFuel1MolValueC, FinalFuel2MolValueC, FinalFuel3MolValueC,
                                                     FinalOxygenMolValueC, FinalAirMolValueC, FinalNitrogenMolValueC, FinalArgonMolValueC);

                FinalEquimolarTextBoxC.Text = Convert.ToString(Math.Round(finalMixtureC.O2toEquimolarO2Value, 2, MidpointRounding.AwayFromZero));
                FinalStoichiometricTextBoxC.Text = Convert.ToString(Math.Round(finalMixtureC.O2toStoichiometricO2Value, 2, MidpointRounding.AwayFromZero));
            }
            else
            {
                FinalEquimolarTextBoxC.Text = "������";
                FinalStoichiometricTextBoxC.Text = "������";
            }
        }
    }
}