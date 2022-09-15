namespace Deton.Fuels
{
    internal class Methane : IFuel
    {
        public string Title => "Methane";

        public string Formula => "CH4";

        public int CarbonAmount => 1;

        public int HydrogenAmount => 4;

        public double Enthalpy => -74.85;

        public override string ToString()
        {
            return "CH4 - Methane";
        }
    }
}