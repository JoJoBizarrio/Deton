namespace Deton.Fuels
{
    internal class Propane : IFuel
    {
        public int CarbonAmount => 3;

        public int HydrogenAmount => 8;

        public double Enthalpy => -103.85;

        public string Formula => "C3H8";

        public string Title => "Propane";

        public override string ToString()
        {
            return "C3H8 - Propane";
        }
    }
}