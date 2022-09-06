using Deton.Fuels;
using Deton.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deton.Graphically;

namespace Deton.Logic
{
    internal class DetonationFunctions
    {
        public DetonationFunctions(double[] Alfa, double[] Beta, IFuel[] initialFuels, IFuel[] finalFuels, int pointsAmount = 20)
        {
            FunctionsPointsCalculator functionsPointsCalculator = new FunctionsPointsCalculator();

            List<double[]> detonationFunctions = new List<double[]>(pointsAmount + 1);

            for (int i = 0; i <= pointsAmount; i++)
            {
                detonationFunctions.Add(new double[16]); 
                detonationFunctions[i] = functionsPointsCalculator.Detka(i, Alfa, Beta, initialFuels, finalFuels);
            }

            Saver.Autosave(detonationFunctions);
        }
    }
}