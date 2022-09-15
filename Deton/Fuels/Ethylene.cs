namespace Deton.Fuels
{
    internal class Ethylene : IFuel
    {
        public int CarbonAmount => 2;

        public int HydrogenAmount => 4;

        public double Enthalpy => 52.47;

        public string Formula => "C2H4";

        public string Title => "Ethylene";

        public override string ToString()
        {
            return "C2H4 - Ethylene";
        }
    }
}