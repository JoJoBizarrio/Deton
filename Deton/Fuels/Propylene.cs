namespace Deton.Fuels
{
    internal class Propylene : IFuel
    {
        public int CarbonAmount => 3;

        public int HydrogenAmount => 6;

        public double Enthalpy => 20.41;

        public override string ToString()
        {
            return "Propylene (C3H6)";
        }
    }
}
