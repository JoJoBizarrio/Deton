using Deton.Fuels;
using Deton.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.System;


namespace Deton.Graphically
{
    internal partial class DetonWiew : Form
    {
        readonly double Epsilon = 0.00000001;

        public DetonWiew(IFuel[] fuels, Variant variantA)
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

            InitialFuelComboBox1.SelectedIndex = 5;
            InitialFuelComboBox2.SelectedIndex = 0;
            InitialFuelComboBox3.SelectedIndex = 0;

            FinalFuelComboBox1.SelectedIndex = 5;
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
                MessageBox.Show("Choose A, B or C.", "Warn: Initial parameters");
                return;
            }

            if (InitialFuelComboBox1.SelectedIndex == 0 && InitialFuelComboBox2.SelectedIndex == 0 && InitialFuelComboBox3.SelectedIndex == 0)
            {
                MessageBox.Show("Choose initial fuels.", "Warn: Initial parameters");
                return;
            }

            if (FinalFuelComboBox1.SelectedIndex == 0 && FinalFuelComboBox2.SelectedIndex == 0 && FinalFuelComboBox3.SelectedIndex == 0)
            {
                MessageBox.Show("Choose final fuels.", "Warn: Initial parameters");
                return;
            }

            IFuel[] initialFuels = new IFuel[] { (IFuel)InitialFuelComboBox1.SelectedItem, (IFuel)InitialFuelComboBox2.SelectedItem, (IFuel)InitialFuelComboBox3.SelectedItem };
            IFuel[] finalFuels = new IFuel[] { (IFuel)FinalFuelComboBox1.SelectedItem, (IFuel)FinalFuelComboBox2.SelectedItem, (IFuel)FinalFuelComboBox3.SelectedItem };

            if (ACheckBox.Checked)
            {
                Mixture initialMixtureA = GetParsedMixture((IFuel)InitialFuelComboBox1.SelectedItem, (IFuel)InitialFuelComboBox2.SelectedItem, (IFuel)InitialFuelComboBox3.SelectedItem,
                                            InitialFuel1MolValueTextBoxA.Text, InitialFuel2MolValueTextBoxA.Text, InitialFuel3MolValueTextBoxA.Text,
                                            InitialOxygenMolValueTextBoxA.Text, InitialAirMolValueTextBoxA.Text, InitialNitrogenMolValueTextBoxA.Text, InitialAirMolValueTextBoxA.Text);

                Mixture finalMixtureA = GetParsedMixture((IFuel)FinalFuelComboBox1.SelectedItem, (IFuel)FinalFuelComboBox2.SelectedItem, (IFuel)FinalFuelComboBox3.SelectedItem,
                                                 FinalFuel1MolValueTextBoxA.Text, FinalFuel2MolValueTextBoxA.Text, FinalFuel3MolValueTextBoxA.Text,
                                                 FinalOxygenMolValueTextBoxA.Text, FinalAirMolValueTextBoxA.Text, FinalNitrogenMolValueTextBoxA.Text, FinalAirMolValueTextBoxA.Text);

                if (IsPossibleCal�ulate(ACheckBox, initialMixtureA, finalMixtureA))
                {
                    DetonationFunctions.CalculateDetonationFunctions(initialMixtureA.ValuesArray, finalMixtureA.ValuesArray, initialFuels, finalFuels);
                    MessageBox.Show("Done of variant A.", "Calculations completed");
                }
            }

            if (BCheckBox.Checked)
            {
                Mixture initialMixtureB = GetParsedMixture((IFuel)InitialFuelComboBox1.SelectedItem, (IFuel)InitialFuelComboBox2.SelectedItem, (IFuel)InitialFuelComboBox3.SelectedItem,
                                                  InitialFuel1MolValueTextBoxB.Text, InitialFuel2MolValueTextBoxB.Text, InitialFuel3MolValueTextBoxB.Text,
                                                  InitialOxygenMolValueTextBoxB.Text, InitialAirMolValueTextBoxB.Text, InitialNitrogenMolValueTextBoxB.Text, InitialAirMolValueTextBoxB.Text);

                Mixture finalMixtureB = GetParsedMixture((IFuel)FinalFuelComboBox1.SelectedItem, (IFuel)FinalFuelComboBox2.SelectedItem, (IFuel)FinalFuelComboBox3.SelectedItem,
                                                 FinalFuel1MolValueTextBoxB.Text, FinalFuel2MolValueTextBoxB.Text, FinalFuel3MolValueTextBoxB.Text,
                                                 FinalOxygenMolValueTextBoxB.Text, FinalAirMolValueTextBoxB.Text, FinalNitrogenMolValueTextBoxB.Text, FinalAirMolValueTextBoxB.Text);

                if (IsPossibleCal�ulate(BCheckBox, initialMixtureB, finalMixtureB))
                {
                    DetonationFunctions.CalculateDetonationFunctions(initialMixtureB.ValuesArray, finalMixtureB.ValuesArray, initialFuels, finalFuels);
                    MessageBox.Show("Done of variant B.", "Calculations completed");
                }
            }

            if (CCheckBox.Checked)
            {
                Mixture initialMixtureC = GetParsedMixture((IFuel)InitialFuelComboBox1.SelectedItem, (IFuel)InitialFuelComboBox2.SelectedItem, (IFuel)InitialFuelComboBox3.SelectedItem,
                                                 InitialFuel1MolValueTextBoxC.Text, InitialFuel2MolValueTextBoxC.Text, InitialFuel3MolValueTextBoxC.Text,
                                                 InitialOxygenMolValueTextBoxC.Text, InitialAirMolValueTextBoxC.Text, InitialNitrogenMolValueTextBoxC.Text, InitialAirMolValueTextBoxC.Text);

                Mixture finalMixtureC = GetParsedMixture((IFuel)FinalFuelComboBox1.SelectedItem, (IFuel)FinalFuelComboBox2.SelectedItem, (IFuel)FinalFuelComboBox3.SelectedItem,
                                                      FinalFuel1MolValueTextBoxC.Text, FinalFuel2MolValueTextBoxC.Text, FinalFuel3MolValueTextBoxC.Text,
                                                      FinalOxygenMolValueTextBoxC.Text, FinalAirMolValueTextBoxC.Text, FinalNitrogenMolValueTextBoxC.Text, FinalAirMolValueTextBoxC.Text);

                if (IsPossibleCal�ulate(CCheckBox, initialMixtureC, finalMixtureC))
                {
                    DetonationFunctions.CalculateDetonationFunctions(initialMixtureC.ValuesArray, finalMixtureC.ValuesArray, initialFuels, finalFuels);
                    MessageBox.Show("Done of variant C.", "Calculations completed");
                }
            }
        }

        private bool IsPossibleCal�ulate(CheckBox variantCheckBox, Mixture initialMixture, Mixture finalMixture)
        {
            bool isPossibleCallulate = true;
            string warningMessage = "";

            if (initialMixture == null)
            {
                warningMessage += "Incorrect entered value(s) of initial mix.";
                isPossibleCallulate = false;
            }
            else if (finalMixture == null)
            {
                warningMessage += "Incorrect entered value(s) of final mix.";
                isPossibleCallulate = false;
            }
            else if (initialMixture.O2toEquimolarO2Value < 1.0 - Epsilon)
            {
                warningMessage += "EqO2 < 1.0 in initial";
                isPossibleCallulate = false;
            }
            else if (finalMixture.O2toEquimolarO2Value < 1.0 - Epsilon)
            {
                warningMessage += "EqO2 < 1.0 in final";
                isPossibleCallulate = false;
            }

            if (!isPossibleCallulate)
            {
                MessageBox.Show(warningMessage, "Warn: Initial parameters of variant " + variantCheckBox.Text);
                return false;
            }

            return true;
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

            Mixture initialMixtureA = GetParsedMixture((IFuel)InitialFuelComboBox1.SelectedItem, (IFuel)InitialFuelComboBox2.SelectedItem, (IFuel)InitialFuelComboBox3.SelectedItem,
                                                  InitialFuel1MolValueTextBoxA.Text, InitialFuel2MolValueTextBoxA.Text, InitialFuel3MolValueTextBoxA.Text,
                                                  InitialOxygenMolValueTextBoxA.Text, InitialAirMolValueTextBoxA.Text, InitialNitrogenMolValueTextBoxA.Text, InitialAirMolValueTextBoxA.Text);

            Mixture initialMixtureB = GetParsedMixture((IFuel)InitialFuelComboBox1.SelectedItem, (IFuel)InitialFuelComboBox2.SelectedItem, (IFuel)InitialFuelComboBox3.SelectedItem,
                                                  InitialFuel1MolValueTextBoxB.Text, InitialFuel2MolValueTextBoxB.Text, InitialFuel3MolValueTextBoxB.Text,
                                                  InitialOxygenMolValueTextBoxB.Text, InitialAirMolValueTextBoxB.Text, InitialNitrogenMolValueTextBoxB.Text, InitialAirMolValueTextBoxB.Text);

            Mixture initialMixtureC = GetParsedMixture((IFuel)InitialFuelComboBox1.SelectedItem, (IFuel)InitialFuelComboBox2.SelectedItem, (IFuel)InitialFuelComboBox3.SelectedItem,
                                                  InitialFuel1MolValueTextBoxC.Text, InitialFuel2MolValueTextBoxC.Text, InitialFuel3MolValueTextBoxC.Text,
                                                  InitialOxygenMolValueTextBoxC.Text, InitialAirMolValueTextBoxC.Text, InitialNitrogenMolValueTextBoxC.Text, InitialAirMolValueTextBoxC.Text);

            Mixture finalMixtureA = GetParsedMixture((IFuel)FinalFuelComboBox1.SelectedItem, (IFuel)FinalFuelComboBox2.SelectedItem, (IFuel)FinalFuelComboBox3.SelectedItem,
                                                  FinalFuel1MolValueTextBoxA.Text, FinalFuel2MolValueTextBoxA.Text, FinalFuel3MolValueTextBoxA.Text,
                                                  FinalOxygenMolValueTextBoxA.Text, FinalAirMolValueTextBoxA.Text, FinalNitrogenMolValueTextBoxA.Text, FinalAirMolValueTextBoxA.Text);

            Mixture finalMixtureB = GetParsedMixture((IFuel)FinalFuelComboBox1.SelectedItem, (IFuel)FinalFuelComboBox2.SelectedItem, (IFuel)FinalFuelComboBox3.SelectedItem,
                                                  FinalFuel1MolValueTextBoxB.Text, FinalFuel2MolValueTextBoxB.Text, FinalFuel3MolValueTextBoxB.Text,
                                                  FinalOxygenMolValueTextBoxB.Text, FinalAirMolValueTextBoxB.Text, FinalNitrogenMolValueTextBoxB.Text, FinalAirMolValueTextBoxB.Text);

            Mixture finalMixtureC = GetParsedMixture((IFuel)FinalFuelComboBox1.SelectedItem, (IFuel)FinalFuelComboBox2.SelectedItem, (IFuel)FinalFuelComboBox3.SelectedItem,
                                                  FinalFuel1MolValueTextBoxC.Text, FinalFuel2MolValueTextBoxC.Text, FinalFuel3MolValueTextBoxC.Text,
                                                  FinalOxygenMolValueTextBoxC.Text, FinalAirMolValueTextBoxC.Text, FinalNitrogenMolValueTextBoxC.Text, FinalAirMolValueTextBoxC.Text);


            SetValueInEqAndStechO2TextBox(InitialEquimolarTextBoxA, InitialStoichiometricTextBoxA, initialMixtureA);
            SetValueInEqAndStechO2TextBox(InitialEquimolarTextBoxB, InitialStoichiometricTextBoxB, initialMixtureB);
            SetValueInEqAndStechO2TextBox(InitialEquimolarTextBoxC, InitialStoichiometricTextBoxC, initialMixtureC);
            SetValueInEqAndStechO2TextBox(FinalEquimolarTextBoxA, FinalStoichiometricTextBoxA, finalMixtureA);
            SetValueInEqAndStechO2TextBox(FinalEquimolarTextBoxB, FinalStoichiometricTextBoxB, finalMixtureB);
            SetValueInEqAndStechO2TextBox(FinalEquimolarTextBoxC, FinalStoichiometricTextBoxC, finalMixtureC);
        }

        private void SetValueInEqAndStechO2TextBox(TextBox O2toEquimolarO2Value, TextBox O2toStoichiometricO2Value, Mixture mixture)
        {
            if (mixture != null)
            {
                O2toEquimolarO2Value.Text = Convert.ToString(Math.Round(mixture.O2toEquimolarO2Value, 2, MidpointRounding.AwayFromZero));
                O2toStoichiometricO2Value.Text = Convert.ToString(Math.Round(mixture.O2toStoichiometricO2Value, 2, MidpointRounding.AwayFromZero));
            }
            else
            {
                O2toEquimolarO2Value.Text = "������";
                O2toStoichiometricO2Value.Text = "������";
            }
        }

        private Mixture GetParsedMixture(IFuel fuel1, IFuel fuel2, IFuel fuel3, string fuel1MolValueText, string fuel2MolValueText, string fuel3MolValueText,
                                         string oxygenMolValueText, string airMolValueText, string nitrogenMolValueText, string argonMolValueText)
        {
            if (double.TryParse(fuel1MolValueText, out double fuel1MolValue) && double.TryParse(fuel2MolValueText, out double fuel2MolValuet) &&
                double.TryParse(fuel3MolValueText, out double fuel3MolValue) && double.TryParse(oxygenMolValueText, out double oxygenMolValue) &&
                double.TryParse(airMolValueText, out double airMolValue) && double.TryParse(nitrogenMolValueText, out double nitrogenMolValue) &&
                double.TryParse(argonMolValueText, out double argonMolValue))
            {
                return new Mixture(fuel1, fuel2, fuel3, fuel1MolValue, fuel2MolValuet, fuel3MolValue, oxygenMolValue, airMolValue, nitrogenMolValue, argonMolValue);
            }

            return null;
        }

        private async void autosaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await Launcher.LaunchFolderPathAsync(Environment.CurrentDirectory + "\\Autosave");
        }
    }
}