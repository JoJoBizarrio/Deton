namespace LIH2022.OrganicCompounds
{
    internal class Ethane : IOrganicCompound
    {
        public string Title => "Ethane";

        public string Formula => "C2H6";

        public int CarbonsAmount => 2;

        public int HydrogensAmount => 6;

        public double Enthalpy => -84.67;

        public override string ToString()
        {
            return "C2H6 - Ethane";
        }
    }
}