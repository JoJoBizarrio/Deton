namespace Deton.Fuels
{
    internal interface IFuel
    {
        int CarbonAmount { get; }

        int HydrogenAmount { get; }

        /// <summary>
        /// Стандартная энтальпия образования в газ фазе при н.у. - кДж/моль
        /// </summary>
        double Enthalpy { get; }

        string Formula { get; }

        string Title { get; }

        string ToString();
    }
}