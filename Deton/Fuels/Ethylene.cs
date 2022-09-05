namespace Deton.Fuels
{
    internal class Ethylene : IFuel
    {
        public int CarbonAmount => 2;

        public int HydrogenAmount => 4;

        public double Enthalpy => 52.47;

        public override string ToString()
        {
            return "Ethylene (C2H4)";
        }
    }
}
