namespace Deton.Fuels
{
    internal class Butene : IFuel
    {
        public int CarbonAmount => 4;

        public int HydrogenAmount => 8;

        public double Enthalpy => -0.13;

        public override string ToString()
        {
            return "Butene (C4H8)";
        }
    }
}
