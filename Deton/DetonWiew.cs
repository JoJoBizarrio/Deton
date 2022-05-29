namespace Deton
{
    public partial class DetonWiew : Form
    {
        public DetonWiew()
        {
            InitializeComponent();

            ACheckBox1.Checked = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void CalculationButton_Click(object sender, EventArgs e)
        {

        }
    }
}