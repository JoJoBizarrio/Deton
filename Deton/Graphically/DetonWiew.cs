using Deton.Fuels;
using Deton.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace Deton.Graphically
{
    internal partial class DetonWiew : Form
    {
        readonly double Epsilon = 0.00000001;

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

            InitialFuelComboBox1.SelectedIndex = 0;
            InitialFuelComboBox2.SelectedIndex = 0;
            InitialFuelComboBox3.SelectedIndex = 0;

            FinalFuelComboBox1.SelectedIndex = 0;
            FinalFuelComboBox2.SelectedIndex = 0;
            FinalFuelComboBox3.SelectedIndex = 0;

            InitialFuel1MolValueTextBoxA.Text = "0";
            FinalFuel1MolValueTextBoxA.Text = "0";

            InitialOxygenMolValueTextBoxA.Text = "0";
            FinalOxygenMolValueTextBoxA.Text = "0";

            string autosavePath = Environment.CurrentDirectory + "\\Autosave";

            if (!File.Exists(autosavePath))
            {
                Directory.CreateDirectory(autosavePath);
            }
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

            CheckBox[] variantsCheckBoxes = new CheckBox[] { ACheckBox, BCheckBox, CCheckBox };
            Conditions[] conditionsList = GetConditions();

            for (int i = 0; i < variantsCheckBoxes.Length; i++)
            {
                if (variantsCheckBoxes[i].Checked && IsPossibleCalñulate(variantsCheckBoxes[i], conditionsList[i]))
                {
                    if (conditionsList[i].InitialMixture.Fuel1MolValue < Epsilon && conditionsList[i].InitialMixture.Fuel2MolValue < Epsilon &&
                        conditionsList[i].InitialMixture.Fuel3MolValue < Epsilon && conditionsList[i].FinalMixture.Fuel1MolValue < Epsilon &&
                        conditionsList[i].FinalMixture.Fuel2MolValue < Epsilon && conditionsList[i].FinalMixture.Fuel3MolValue < Epsilon &&
                        conditionsList[i].InitialMixture.AirMolValue < Epsilon && conditionsList[i].InitialMixture.OxygenMolValue < Epsilon &&
                        conditionsList[i].InitialMixture.ArgonMolValue < Epsilon && conditionsList[i].InitialMixture.NitrogenMolValue < Epsilon &&
                        conditionsList[i].FinalMixture.AirMolValue < Epsilon && conditionsList[i].FinalMixture.OxygenMolValue < Epsilon &&
                        conditionsList[i].FinalMixture.ArgonMolValue < Epsilon && conditionsList[i].FinalMixture.NitrogenMolValue < Epsilon)
                    {
                        continue;
                    }

                    DetonationFunctions.CalculateDetonationFunctions(conditionsList[i]);
                    MessageBox.Show($"Done of variant {variantsCheckBoxes[i].Text}.", "Calculations completed");
                }
            }
        }

        private Conditions[] GetConditions()
        {
            List<string[]> variantsInitialValuesStringsArrayList = new List<string[]>
            {
                new string[]
                {
                    InitialFuel1MolValueTextBoxA.Text, InitialFuel2MolValueTextBoxA.Text, InitialFuel3MolValueTextBoxA.Text,
                    InitialOxygenMolValueTextBoxA.Text, InitialAirMolValueTextBoxA.Text, InitialNitrogenMolValueTextBoxA.Text, InitialArgonMolValueTextBoxA.Text
                },

                new string[]
                {
                    InitialFuel1MolValueTextBoxB.Text, InitialFuel2MolValueTextBoxB.Text, InitialFuel3MolValueTextBoxB.Text,
                    InitialOxygenMolValueTextBoxB.Text, InitialAirMolValueTextBoxB.Text, InitialNitrogenMolValueTextBoxB.Text, InitialArgonMolValueTextBoxB.Text,
                },

                new string[]
                {
                    InitialFuel1MolValueTextBoxC.Text, InitialFuel2MolValueTextBoxC.Text, InitialFuel3MolValueTextBoxC.Text,
                    InitialOxygenMolValueTextBoxC.Text, InitialAirMolValueTextBoxC.Text, InitialNitrogenMolValueTextBoxC.Text, InitialArgonMolValueTextBoxC.Text,
                },

            };

            List<string[]> variantsFinalValuesStringsArrayList = new List<string[]>
            {
                 new string[]
                 {
                     FinalFuel1MolValueTextBoxA.Text, FinalFuel2MolValueTextBoxA.Text, FinalFuel3MolValueTextBoxA.Text,
                     FinalOxygenMolValueTextBoxA.Text, FinalAirMolValueTextBoxA.Text, FinalNitrogenMolValueTextBoxA.Text, FinalArgonMolValueTextBoxA.Text,
                 },

                 new string[]
                 {
                     FinalFuel1MolValueTextBoxB.Text, FinalFuel2MolValueTextBoxB.Text, FinalFuel3MolValueTextBoxB.Text,
                     FinalOxygenMolValueTextBoxB.Text, FinalAirMolValueTextBoxB.Text, FinalNitrogenMolValueTextBoxB.Text, FinalArgonMolValueTextBoxB.Text,
                 },

                 new string[]
                 {
                     FinalFuel1MolValueTextBoxC.Text, FinalFuel2MolValueTextBoxC.Text, FinalFuel3MolValueTextBoxC.Text,
                     FinalOxygenMolValueTextBoxC.Text, FinalAirMolValueTextBoxC.Text, FinalNitrogenMolValueTextBoxC.Text, FinalArgonMolValueTextBoxC.Text
                 }
            };

            IFuel[] initialFuels = new IFuel[] { (IFuel)InitialFuelComboBox1.SelectedItem, (IFuel)InitialFuelComboBox2.SelectedItem, (IFuel)InitialFuelComboBox3.SelectedItem };
            IFuel[] finalFuels = new IFuel[] { (IFuel)FinalFuelComboBox1.SelectedItem, (IFuel)FinalFuelComboBox2.SelectedItem, (IFuel)FinalFuelComboBox3.SelectedItem };

            Conditions[] conditionsList = new Conditions[3];

            for (int i = 0; i < 3; i++)
            {
                conditionsList[i] = new Conditions(GetParsedMixture(initialFuels, variantsInitialValuesStringsArrayList[i]),
                                                   GetParsedMixture(finalFuels, variantsFinalValuesStringsArrayList[i]));
            }

            return conditionsList;
        }

        static private Mixture GetParsedMixture(IFuel[] fuels, string[] compoundsMolsStringsArray)
        {
            double[] compoundsMolsValuesArray = new double[compoundsMolsStringsArray.Length];

            for (int i = 0; i < compoundsMolsValuesArray.Length; i++)
            {
                if (!double.TryParse(compoundsMolsStringsArray[i], out compoundsMolsValuesArray[i]))
                {
                    return null;
                }
            }

            return new Mixture(fuels, compoundsMolsValuesArray);
        }

        private bool IsPossibleCalñulate(CheckBox variantCheckBox, Conditions conditions)
        {
            bool isPossibleCalculate = true;
            string warningMessage = "";

            if (conditions.InitialMixture == null)
            {
                warningMessage += "Incorrect entered value(s) of initial mixture.";
                isPossibleCalculate = false;
            }
            else if (conditions.FinalMixture == null)
            {
                warningMessage += "Incorrect entered value(s) of final mixture.";
                isPossibleCalculate = false;
            }
            else if (conditions.InitialMixture.O2toEquimolarO2Value < 1.0 + Epsilon)
            {
                warningMessage += "Surplace of oxigen less 1.0 of initial mixture.";
                isPossibleCalculate = false;
            }
            else if (conditions.FinalMixture.O2toEquimolarO2Value < 1.0 + Epsilon)
            {
                warningMessage += "Surplace of oxigen less 1.0 of final mixture.";
                isPossibleCalculate = false;
            }

            if (!isPossibleCalculate)
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

            Conditions[] conditionsList = GetConditions();

            TextBox[] InitialEquimolarTextBoxs = new TextBox[] { InitialEquimolarTextBoxA, InitialEquimolarTextBoxB, InitialEquimolarTextBoxC };
            TextBox[] InitialStoichiometricTextBoxs = new TextBox[] { InitialStoichiometricTextBoxA, InitialStoichiometricTextBoxB, InitialStoichiometricTextBoxC };

            TextBox[] FinalEquimolarTextBoxs = new TextBox[] { FinalEquimolarTextBoxA, FinalEquimolarTextBoxB, FinalEquimolarTextBoxC };
            TextBox[] FinalStoichiometricTextBoxs = new TextBox[] { FinalStoichiometricTextBoxA, FinalStoichiometricTextBoxB, FinalStoichiometricTextBoxC };

            for (int i = 0; i < conditionsList.Length; i++)
            {
                SetValueInEqAndStechO2TextBox(InitialEquimolarTextBoxs[i], InitialStoichiometricTextBoxs[i], conditionsList[i].InitialMixture);
                SetValueInEqAndStechO2TextBox(FinalEquimolarTextBoxs[i], FinalStoichiometricTextBoxs[i], conditionsList[i].FinalMixture);
            }
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
                O2toEquimolarO2Value.Text = "fault";
                O2toStoichiometricO2Value.Text = "fault";
            }
        }

        //private async void autosaveToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    await Windows.System.Launcher.LaunchFolderPathAsync(Environment.CurrentDirectory + "\\Autosave");
        //}

        private void autosaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process auvtosaveOpener = new Process();
            auvtosaveOpener.StartInfo.FileName = "explorer";
            auvtosaveOpener.StartInfo.Arguments = Environment.CurrentDirectory + "\\Autosave";
            auvtosaveOpener.Start();
        }
    }
}