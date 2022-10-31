namespace LIH2022.OrganicCompounds
{
    internal class Methane : IOrganicCompound
    {
        public string Title => "Methane";

        public string Formula => "CH4";

        public int CarbonsAmount => 1;

        public int HydrogensAmount => 4;

        public double Enthalpy => -74.85;

        public override string ToString()
        {
            return "CH4 - Methane";
        }
    }
}