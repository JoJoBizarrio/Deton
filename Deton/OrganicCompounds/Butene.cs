namespace LIH2022.OrganicCompounds
{
    internal class Butene : IOrganicCompound
    {
        public string Title => "Butene";

        public string Formula => "C4H8";

        public int CarbonsAmount => 4;

        public int HydrogensAmount => 8;

        public double Enthalpy => -0.13;

        public override string ToString()
        {
            return "C4H8 - Butene";
        }
    }
}