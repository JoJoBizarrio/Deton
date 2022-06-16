namespace Deton.Fuels
{
    internal class Ethane : IFuel
    {
        public int CarbonAmount => 2;

        public int HydrogenAmount => 6;

        public double Enthalpy => -84.67;

        public override string ToString()
        {
            return "C2H6";
        }
    }
}
