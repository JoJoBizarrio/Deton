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
    internal class Variant
    {
        public ComboBox ComboBox1f { get; set; }
        ComboBox comboBox2f { get; set; }
        ComboBox comboBox3f { get; set; }

        TextBox textBox1f { get; set; }
        TextBox textBox2f { get; set; }
        TextBox textBox3f { get; set; }

        TextBox textBoxO2 { get; set; }
        TextBox textBoxAir { get; set; }




        public Variant()
        {
            
        }

        public Variant(ComboBox comboBox)
        {
            ComboBox1f = comboBox;
        }
    }
}
