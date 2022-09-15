namespace Deton.Fuels
{
    internal class Butene : IFuel
    {
        public int CarbonAmount => 4;

        public int HydrogenAmount => 8;

        public double Enthalpy => -0.13;

        public string Formula => "C4H8";

        public string Title => "Butene";

        public override string ToString()
        {
            return "C4H8 - Butene";
        }
    }
}