using System.Text;
using Deton.Fuels;

namespace Deton.Logic
{
    internal class Mixture
    {
        public IFuel Fuel1 { get; }
        public IFuel Fuel2 { get; }
        public IFuel Fuel3 { get; }

        public readonly IFuel[] FuelsArray = new IFuel[3];

        public double Fuel1MolValue { get; }

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

            FuelsArray[0] = Fuel1;
            FuelsArray[1] = Fuel2;
            FuelsArray[2] = Fuel3;
        }

        public Mixture(IFuel[] fuels, double[] compoundsMolsValuesArray)
        {
            Fuel1 = fuels[0];
            Fuel2 = fuels[1];
            Fuel3 = fuels[2];

            FuelsArray[0] = Fuel1;
            FuelsArray[1] = Fuel2;
            FuelsArray[2] = Fuel3;

            Fuel1MolValue = compoundsMolsValuesArray[0];
            Fuel2MolValue = compoundsMolsValuesArray[1];
            Fuel3MolValue = compoundsMolsValuesArray[2];
            OxygenMolValue = compoundsMolsValuesArray[3];
            AirMolValue = compoundsMolsValuesArray[4];
            NitrogenMolValue = compoundsMolsValuesArray[5];
            ArgonMolValue = compoundsMolsValuesArray[6];

            for (int i = 0; i < fuels.Length; i++)
            {
                if (FuelsArray[i].GetType() != new NotSelected().GetType() && compoundsMolsValuesArray[i] == 0)
                {
                    ValuesArray[i] = double.Epsilon;
                }
                else
                {
                    ValuesArray[i] = compoundsMolsValuesArray[i];
                }
            }

            for (int i = fuels.Length; i < compoundsMolsValuesArray.Length; i++)
            {
                ValuesArray[i] = compoundsMolsValuesArray[i];
            }

            double CarbonsAmount = Fuel1MolValue * Fuel1.CarbonAmount + Fuel2MolValue * Fuel2.CarbonAmount + Fuel3MolValue * Fuel3.CarbonAmount;
            double HydrogenAmount = Fuel1MolValue * Fuel1.HydrogenAmount + Fuel2MolValue * Fuel2.HydrogenAmount + Fuel3MolValue * Fuel3.HydrogenAmount;
            double OxygenAmount = 2 * (OxygenMolValue + 0.20954 * AirMolValue);

            O2toEquimolarO2Value = OxygenAmount / CarbonsAmount;
            O2toStoichiometricO2Value = OxygenAmount / (2 * CarbonsAmount + 0.5 * HydrogenAmount);
        }

        public Mixture(IFuel fuel1, IFuel fuel2, IFuel fuel3, double fuel1MolValue, double fuel2MolValue, double fuel3MolValue,
                       double oxygenMolValue, double airMolValue, double nitrogenMolValue, double argonMolValue)
        {
            Fuel1 = fuel1;
            Fuel2 = fuel2;
            Fuel3 = fuel3;

            FuelsArray[0] = Fuel1;
            FuelsArray[1] = Fuel2;
            FuelsArray[2] = Fuel3;

            Fuel1MolValue = fuel1MolValue;
            Fuel2MolValue = fuel2MolValue;
            Fuel3MolValue = fuel3MolValue;
            OxygenMolValue = oxygenMolValue;
            AirMolValue = airMolValue;
            NitrogenMolValue = nitrogenMolValue;
            ArgonMolValue = argonMolValue;

            for (int i = 0; i < FuelsArray.Length; i++)
            {
                if (FuelsArray[i].GetType() != new NotSelected().GetType() && ValuesArray[i] == 0)
                {
                    ValuesArray[i] = double.Epsilon;
                }
            }

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
            string plus = " + ";

            if (Fuel1.GetType() != new NotSelected().GetType() || Fuel1MolValue != 0)
            {
                stringBuilder.Append(Fuel1MolValue.ToString("F2"));
                stringBuilder.Append(Fuel1.Formula);
                stringBuilder.Append(plus);
            }

            if (Fuel2.GetType() != new NotSelected().GetType() || Fuel2MolValue != 0)
            {
                stringBuilder.Append(Fuel2MolValue.ToString("F2"));
                stringBuilder.Append(Fuel2.Formula);
                stringBuilder.Append(plus);
            }

            if (Fuel3.GetType() != new NotSelected().GetType() || Fuel3MolValue != 0)
            {
                stringBuilder.Append(Fuel3MolValue.ToString("F2"));
                stringBuilder.Append(Fuel3.Formula);
                stringBuilder.Append(plus);
            }

            if (OxygenMolValue != 0)
            {
                stringBuilder.Append(OxygenMolValue.ToString("F2"));
                stringBuilder.Append("O2");
                stringBuilder.Append(plus);
            }

            if (AirMolValue != 0)
            {
                stringBuilder.Append(AirMolValue.ToString("F2"));
                stringBuilder.Append("Air");
                stringBuilder.Append(plus);
            }

            if (NitrogenMolValue != 0)
            {
                stringBuilder.Append(NitrogenMolValue.ToString("F2"));
                stringBuilder.Append("N2");
                stringBuilder.Append(plus);
            }

            if (ArgonMolValue != 0)
            {
                stringBuilder.Append(ArgonMolValue.ToString("F2"));
                stringBuilder.Append("Ar");
                stringBuilder.Append(plus);
            }

            stringBuilder.Remove(stringBuilder.Length - plus.Length, plus.Length);

            return stringBuilder.ToString();
        }
    }
}