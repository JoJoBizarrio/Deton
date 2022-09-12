using System;
using System.Collections.Generic;
using System.Text;
using Deton.Fuels;

namespace Deton.Logic
{
    internal class Mixture
    {
        public IFuel Fuel1 { get; }
        public IFuel Fuel2 { get; }
        public IFuel Fuel3 { get; }

        public double Fuel1MolValue { get; set; }
        public double Fuel2MolValue { get; }
        public double Fuel3MolValue { get; }

        public double OxygenMolValue { get; }
        public double AirMolValue { get; }

        public double NitrogenMolValue { get; }
        public double ArgonMolValue { get; }

        public double O2toEquimolarO2Value { get; }
        public double O2toStoichiometricO2Value { get; }

        public double[] ValuesArray = new double[7];

        public Mixture()
        {
            Fuel1 = new NotSelected();
            Fuel2 = new NotSelected();
            Fuel3 = new NotSelected();
        }

        public Mixture(IFuel fuel1, IFuel fuel2, IFuel fuel3, double fuel1MolValue, double fuel2MolValue, double fuel3MolValue,
                       double oxygenMolValue, double airMolValue, double nitrogenMolValue = 0, double argonMolValue = 0)
        {
            Fuel1 = fuel1;
            Fuel2 = fuel2;
            Fuel3 = fuel3;
            Fuel1MolValue = fuel1MolValue;
            Fuel2MolValue = fuel2MolValue;
            Fuel3MolValue = fuel3MolValue;
            OxygenMolValue = oxygenMolValue;
            AirMolValue = airMolValue;
            NitrogenMolValue = nitrogenMolValue;
            ArgonMolValue = argonMolValue;

            ValuesArray[0] = fuel1MolValue;
            ValuesArray[1] = fuel2MolValue;
            ValuesArray[2] = fuel3MolValue;
            ValuesArray[3] = oxygenMolValue;
            ValuesArray[4] = airMolValue;
            ValuesArray[5] = nitrogenMolValue;
            ValuesArray[6] = argonMolValue;

            double CarbonsAmount = Fuel1MolValue * Fuel1.CarbonAmount + Fuel2MolValue * Fuel2.CarbonAmount + Fuel3MolValue * Fuel3.CarbonAmount;
            double HydrogenAmount = Fuel1MolValue * Fuel1.HydrogenAmount + Fuel2MolValue * Fuel2.HydrogenAmount + Fuel3MolValue * Fuel3.HydrogenAmount;
            double OxygenAmount = 2 * (OxygenMolValue + 0.20954 * AirMolValue);

            O2toEquimolarO2Value = OxygenAmount / CarbonsAmount;
            O2toStoichiometricO2Value = OxygenAmount / (2 * CarbonsAmount + 0.5 * HydrogenAmount);
        }
    }
}