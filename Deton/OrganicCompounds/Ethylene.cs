namespace LIH2022.OrganicCompounds
{
    internal class Ethylene : IOrganicCompound
    {
        public string Title => "Ethylene";

        public string Formula => "C2H4";

        public int CarbonsAmount => 2;

        public int HydrogensAmount => 4;

        public double Enthalpy => 52.47;

        public override string ToString()
        {
            return "C2H4 - Ethylene";
        }
    }
}