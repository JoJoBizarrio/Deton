namespace Deton.Fuels
{
    internal class Propylene : IFuel
    {
        public int CarbonAmount => 3;

        public int HydrogenAmount => 6;

        public double Enthalpy => 20.41;

        public string Formula => "C3H6";

        public string Title => "Propylene";

        public override string ToString()
        {
            return "C3H6 - Propylene";
        }
    }
}