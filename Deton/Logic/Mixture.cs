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

        public readonly IFuel[] fuelsArray = new IFuel[3];

        public double Fuel1MolValue { get; set; }
        public double Fuel2MolValue { get; }
        public double Fuel3MolValue { get; }

        public double OxygenMolValue { get; }
        public double AirMolValue { get; }

        public double NitrogenMolValue { get; }
        public double ArgonMolValue { get; }

        public double O2toEquimolarO2Value { get; }
        public double O2toStoichiometricO2Value { get; }

        public readonly double[] ValuesArray = new double[7];

        public Mixture()
        {
            Fuel1 = new NotSelected();
            Fuel2 = new NotSelected();
            Fuel3 = new NotSelected();

            fuelsArray[0] = Fuel1;
            fuelsArray[1] = Fuel2;
            fuelsArray[2] = Fuel3;
        }

        public Mixture(IFuel fuel1, IFuel fuel2, IFuel fuel3, double fuel1MolValue, double fuel2MolValue, double fuel3MolValue,
                       double oxygenMolValue, double airMolValue, double nitrogenMolValue = 0, double argonMolValue = 0)
        {
            Fuel1 = fuel1;
            Fuel2 = fuel2;
            Fuel3 = fuel3;

            fuelsArray[0] = Fuel1;
            fuelsArray[1] = Fuel2;
            fuelsArray[2] = Fuel3;

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

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (Fuel1 != new NotSelected())
            {
                stringBuilder.Append(Fuel1MolValue);
                stringBuilder.Append(Fuel1);
                stringBuilder.Append(" + ");
            }

            if (Fuel2 != new NotSelected())
            {
                stringBuilder.Append(Fuel2MolValue);
                stringBuilder.Append(Fuel2);
                stringBuilder.Append(" + ");
            }

            if (Fuel3 != new NotSelected())
            {
                stringBuilder.Append(Fuel3MolValue);
                stringBuilder.Append(Fuel3);
                stringBuilder.Append(" + ");
            }

            // string mixture = $"{Fuel1MolValue}{Fuel1} + {Fuel2MolValue}{Fuel2} + {Fuel3MolValue}{Fuel3} + {OxygenMolValue}O2 + {AirMolValue}Air + {NitrogenMolValue}N2 + {ArgonMolValue}Ar";
            //mixture.Replace("Not selected", "");

            if (OxygenMolValue != 0)
            {
                stringBuilder.Append(" + ");
                stringBuilder.Append(OxygenMolValue);
                stringBuilder.Append("O2");
            }

            if (AirMolValue != 0)
            {
                stringBuilder.Append(" + ");
                stringBuilder.Append(AirMolValue);
                stringBuilder.Append("Air");
            }

            if (NitrogenMolValue !=  0)
            {
                stringBuilder.Append(" + ");
                stringBuilder.Append(NitrogenMolValue);
                stringBuilder.Append("N2");
            }

            if (ArgonMolValue != 0)
            {
                stringBuilder.Append(" + ");
                stringBuilder.Append(ArgonMolValue);
                stringBuilder.Append("Ar");
            }

            return stringBuilder.ToString();
        }
    }
}