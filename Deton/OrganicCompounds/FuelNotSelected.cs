using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIH2022.OrganicCompounds
{
    internal class FuelNotSelected : IOrganicCompound
    {
        /// <summary>
        /// return: Not selected
        /// </summary>
        public string Title => "Not selected";

        /// <summary>
        /// return: ""
        /// </summary>
        public string Formula => "";

        // от топлива:
        public int CarbonsAmount => 0;

        public int HydrogensAmount => 0;

        public double Enthalpy => 0.0;

        public override string ToString()
        {
            return Title;
        }
    }
}