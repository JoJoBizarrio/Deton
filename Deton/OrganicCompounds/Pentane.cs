namespace LIH2022.OrganicCompounds
{
    internal class Pentane : IOrganicCompound
    {
        public string Title => "Pentane";

        public string Formula => "C5H12";

        public int CarbonsAmount => 5;

        public int HydrogensAmount => 12;

        public double Enthalpy => -146.77;

        public override string ToString()
        {
            return "C5H12 - Pentane";
        }
    }
}