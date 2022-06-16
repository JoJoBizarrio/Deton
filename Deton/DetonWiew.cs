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
                double[] alfa = new double[7];
                double[] beta = new double[7];

                alfa[0] = Convert.ToDouble(InitialFuel1MolValueTextBoxA.Text);
                alfa[1] = Convert.ToDouble(InitialFuel2MolValueTextBoxA.Text);
                alfa[2] = Convert.ToDouble(InitialFuel3MolValueTextBoxA.Text);
                alfa[3] = Convert.ToDouble(InitialOxygenMolValueTextBoxA.Text);
                alfa[4] = Convert.ToDouble(InitialAirMolValueTextBoxA.Text);
                alfa[5] = Convert.ToDouble(InitialArgonMolValueTextBoxA.Text);
                alfa[6] = Convert.ToDouble(InitialNitrogenMolValueTextBoxA.Text);

                beta[0] = Convert.ToDouble(FinalFuel1MolValueTextBoxA.Text);
                beta[1] = Convert.ToDouble(FinalFuel2MolValueTextBoxA.Text);
                beta[2] = Convert.ToDouble(FinalFuel3MolValueTextBoxA.Text);
                beta[3] = Convert.ToDouble(FinalOxygenMolValueTextBoxA.Text);
                beta[4] = Convert.ToDouble(FinalAirMolValueTextBoxA.Text);
                beta[5] = Convert.ToDouble(FinalArgonMolValueTextBoxA.Text);
                beta[6] = Convert.ToDouble(FinalNitrogenMolValueTextBoxA.Text);

                IFuel[] initialFuels = new IFuel[] { (IFuel)initialFuelComboBox1.SelectedItem, (IFuel)initialFuelComboBox2.SelectedItem, (IFuel)initialFuelComboBox3.SelectedItem };
                IFuel[] finalFuels = new IFuel[] { (IFuel)finalFuelComboBox1.SelectedItem, (IFuel)finalFuelComboBox2.SelectedItem, (IFuel)finalFuelComboBox3.SelectedItem };

                DetonationFunctions detonationFunctions = new DetonationFunctions(alfa, beta, initialFuels, finalFuels);

                //using StreamWriter streamWriter = new StreamWriter("../result.txt");
                //{
                //    for (int i = 0; i < Fun.GetLength(2); i++)
                //    {
                //        for (int j = 0; j < Fun.GetLength(1); j++)
                //        {
                //            streamWriter.WriteLine(Fun[v, j, i]);
                //        }

                //        streamWriter.WriteLine();
                //        streamWriter.WriteLine();
                //    }

                  MessageBox.Show("Done.");
                //}

            }
        }
    }
}