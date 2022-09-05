using System;
using System.Collections.Generic;
using System.Text;
using Deton.Fuels;

namespace Deton.Logic
{
    internal class Conditions
    {
        IFuel InitialFuel1 { get; }
        IFuel InitialFuel2 { get; }
        IFuel InitialFuel3 { get; }

        double InitialFuel1MolValue { get; }
        double InitialFuel2MolValue { get; }
        double InitialFuel3MolValue { get; }

        double InitialOxygenMolValue { get; }
        double InitialAirMolValue { get; }

        double InitialNitrogenMolValue { get; }
        double InitialArgonMolValue { get; }

        IFuel FinalFuel1 { get; }
        IFuel FinalFuel2 { get; }
        IFuel FinalFuel3 { get; }

        double FinalFuel1MolValue { get; }
        double FinalFuel2MolValue { get; }
        double FinalFuel3MolValue { get; }

        double FinalOxygenMolValue { get; }
        double FinalAirMolValue { get; }

        double FinalNitrogenMolValue { get; }
        double FinalArgonMolValue { get; }


        public Conditions(IFuel InitialFuel1, IFuel InitialFuel2, IFuel InitialFuel3, double InitialFuel1MolValue, double InitialFuel2MolValue, double InitialFuel3MolValue, double InitialOxygenMolValue)
        {

        }

    }
}