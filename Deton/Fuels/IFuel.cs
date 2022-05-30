namespace Deton.Fuels
{
    internal interface IFuel
    {
        int CarbonAmount { get; }

        int HydrogenAmount { get; }

        string ToString();
    }
}