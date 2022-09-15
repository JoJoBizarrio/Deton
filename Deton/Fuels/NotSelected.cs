namespace Deton.Fuels
{
    internal class NotSelected : IFuel
    {
        /// <summary>
        /// return: "Not selected"
        /// </summary>
        public string Title => "Not selected";

        /// <summary>
        /// return: ""
        /// </summary>
        public string Formula => "";

        public int CarbonAmount => 0;

        public int HydrogenAmount => 0;

        public double Enthalpy => 0;



        public override string ToString()
        {
            return "Not selected";
        }
    }
}