namespace Deton.Fuels
{
    internal interface IFuel
    {
        int CarbonAmount { get; }

        int HydrogenAmount { get; }

        /// <summary>
        /// Стандартная энтропия образования при н.у. - кДж/(моль*К)
        /// </summary>
        double Entropy { get; }

        string ToString();
    }
}