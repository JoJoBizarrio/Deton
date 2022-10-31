namespace LIH2022.OrganicCompounds
{
    internal class Hydrogen : IOrganicCompound
    {
        public string Title => "Hydrogen";

        public string Formula => "H2";

        public int CarbonsAmount => 0;

        public int HydrogensAmount => 2;

        public double Enthalpy => 0.0;

        public override string ToString()
        {
            return "H2 - Hydrogen";
        }
    }
}