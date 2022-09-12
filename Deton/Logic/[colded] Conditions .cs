using System;
using System.Collections.Generic;
using System.Text;
using Deton.Fuels;

namespace Deton.Logic
{
    internal class Conditions
    {
        Mixture Initial { get; set; }

        Mixture Final { get; set; }

        public Conditions(Mixture initial, Mixture final)
        {
            Initial = initial;
            Final = final;
        }
    }
}