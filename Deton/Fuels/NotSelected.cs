namespace Deton.Fuels
{
    internal class NotSelected : IFuel
    {
        public int HydrogenAmount => 0;

        public int CarbonAmount => 0;

        public double Entropy => 0;

        public override string ToString()
        {
            return "Not selected";
        }
    }
}
