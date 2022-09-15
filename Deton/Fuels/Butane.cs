namespace Deton.Fuels
{
    internal class Butane : IFuel
    {
        public int CarbonAmount => 4;

        public int HydrogenAmount => 10;

        public double Enthalpy => -126.15;

        public string Formula => "C4H10";

        public string Title => "Butane";

        public override string ToString()
        {
            return "C4H10 - Butane";
        }
    }
}