namespace Deton.Fuels
{
    internal class Hydrogen : IFuel
    {
        public int CarbonAmount => 0;

        public int HydrogenAmount => 2;

        public double Enthalpy => 0.0;

        public string Formula => "H2";

        public string Title => "Hydrogen";

        public override string ToString()
        {
            return "H2 - Hydrogen";
        }
    }
}