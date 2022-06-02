namespace Deton.Fuels
{
    internal class Acetylene : IFuel
    {
        public int HydrogenAmount => 2;

        public int CarbonAmount => 2;

        public double Entropy => 0.200927;

        public override string ToString()
        {
            return "C2H2";
        }
    }
}
