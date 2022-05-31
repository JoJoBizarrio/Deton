﻿namespace Deton.Fuels
{
    internal class Hydrogen : IFuel
    {
        public int HydrogenAmount => 2;

        public int CarbonAmount => 0;

        public double Entropy => 0.13052;

        public override string ToString()
        {
            return "H2";
        }
    }
}