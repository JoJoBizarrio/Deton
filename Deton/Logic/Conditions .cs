namespace Deton.Logic
{
    internal class Conditions
    {
        public Mixture InitialMixture { get; set; }

        public Mixture FinalMixture { get; set; }

        public Conditions(Mixture initialMixture, Mixture finalMixture)
        {
            InitialMixture = initialMixture;
            FinalMixture = finalMixture;
        }

        public override string ToString()
        {
            return $"{InitialMixture}  =>  {FinalMixture}"; 
        }
    }
}