namespace Deton.Logic
{
    internal class Conditions
    {
        public Mixture InitialMixture { get; }

        public Mixture FinalMixture { get; }

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