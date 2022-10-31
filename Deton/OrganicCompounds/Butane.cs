namespace LIH2022.OrganicCompounds
{
    internal class Butane : IOrganicCompound
    {
        public string Title => "Butane";

        public string Formula => "C4H10";

        public int CarbonsAmount => 4;

        public int HydrogensAmount => 10;

        public double Enthalpy => -126.15;

        public override string ToString()
        {
            return "C4H10 - Butane";
        }
    }
}