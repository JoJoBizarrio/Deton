namespace Deton.Fuels
{
    internal class Pentane : IFuel
    {
        public string Title => "Pentane";

        public string Formula => "C5H12";

        public int CarbonAmount => 5;

        public int HydrogenAmount => 12;

        public double Enthalpy => -173.33;

        public override string ToString()
        {
            return "C5H12 - Pentane";
        }
    }
}