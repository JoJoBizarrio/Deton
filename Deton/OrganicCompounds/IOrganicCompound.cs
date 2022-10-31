namespace LIH2022.OrganicCompounds
{
    internal interface IOrganicCompound
    {
        string Title { get; }

        string Formula { get; }

        int CarbonsAmount { get; }

        int HydrogensAmount { get; }

        /// <summary>
        /// Стандартная энтальпия образования в газ фазе при н.у. - кДж/моль
        /// </summary>
        double Enthalpy { get; }

        string ToString();
    }
}