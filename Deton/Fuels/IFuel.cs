namespace Deton.Fuels
{
    internal interface IFuel
    {
        int CarbonAmount { get; }

        int HydrogenAmount { get; }

        /// <summary>
        /// Стандартная энтальпия образования при н.у. - кДж/(моль*К)
        /// </summary>
        double Enthalpy { get; }

        string ToString();
    }
}