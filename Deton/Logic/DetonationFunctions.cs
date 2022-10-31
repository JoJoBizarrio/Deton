using System.Collections.Generic;

using Deton.Graphically;

namespace Deton.Logic
{
    internal class DetonationFunctions
    {
        public static void CalculateDetonationFunctions(Conditions conditions, int pointsAmount = 20)
        {
            FunctionsPointsCalculator functionsPointsCalculator = new FunctionsPointsCalculator();

            List<double[]> detonationFunctions = new List<double[]>(pointsAmount + 1);

            for (int i = 0; i <= pointsAmount; i++)
            {
                detonationFunctions.Add(new double[16]);
                detonationFunctions[i] = functionsPointsCalculator.Detka(i, conditions);
            }

            Saver.Autosave(detonationFunctions, conditions);
        }
    }
}