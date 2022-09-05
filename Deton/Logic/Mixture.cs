using System;
using System.Collections.Generic;
using System.Text;
using Deton.Fuels;

namespace Deton.Logic
{
    internal class Mixture
    {
        IFuel Fuel1 = new NotSelected();
        IFuel Fuel2 = new NotSelected();
        IFuel Fuel3 = new NotSelected();

        public double Fuel1MolValue { get; }
        public double Fuel2MolValue { get; }
        public double Fuel3MolValue { get; }

        public double OxygenMolValue { get; }
        public double AirMolValue { get; }
    
        public double NitrogenMolValue { get; }
        public double ArgonMolValue { get; }

        public double O2toEquimolarO2Value { get;}
        public double O2toStoichiometricO2Value { get; }

        public Mixture(IFuel fuel1, IFuel fuel2, IFuel fuel3, double fuel1MolValue, double fuel2MolValue, double fuel3MolValue,
                       double oxygenMolValue, double airMolValue, double nitrogenMolValue, double argonMolValue)
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

            double CarbonsAmount = fuel1MolValue * Fuel1.CarbonAmount + fuel2MolValue * Fuel2.CarbonAmount + fuel3MolValue * Fuel3.CarbonAmount;
            double HydrogenAmount = fuel1MolValue * Fuel1.HydrogenAmount + fuel2MolValue * Fuel2.HydrogenAmount + fuel3MolValue * Fuel3.HydrogenAmount;
            double OxygenAmount = 2 * (oxygenMolValue + 0.20954 * airMolValue);

            O2toEquimolarO2Value = OxygenAmount / CarbonsAmount;
            O2toStoichiometricO2Value = OxygenAmount / (2 * CarbonsAmount + 0.5 * HydrogenAmount);
        }
    }
}