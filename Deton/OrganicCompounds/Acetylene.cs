namespace LIH2022.OrganicCompounds
{
    internal class Acetylene : IOrganicCompound
    {
        public string Title => "Acetylene";

        public string Formula => "C2H2";

        public int CarbonsAmount => 2;

        public int HydrogensAmount => 2;

        public double Enthalpy => 226.75;

        public override string ToString()
        {
            return "C2H2 - Acetylene";
        }
    }
}