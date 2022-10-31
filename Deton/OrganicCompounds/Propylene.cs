namespace LIH2022.OrganicCompounds
{
    internal class Propylene : IOrganicCompound
    {
        public string Title => "Propylene";

        public string Formula => "C3H6";

        public int CarbonsAmount => 3;

        public int HydrogensAmount => 6;

        public double Enthalpy => 20.41;

        public override string ToString()
        {
            return "C3H6 - Propylene";
        }
    }
}