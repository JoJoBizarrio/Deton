namespace LIH2022.OrganicCompounds
{
    internal class Propane : IOrganicCompound
    {
        public string Title => "Propane";
        public string Formula => "C3H8";

        public int CarbonsAmount => 3;

        public int HydrogensAmount => 8;

        public double Enthalpy => -103.85;

        public override string ToString()
        {
            return "C3H8 - Propane";
        }
    }
}