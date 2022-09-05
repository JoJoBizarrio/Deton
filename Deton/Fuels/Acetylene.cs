﻿namespace Deton.Fuels
{
    internal class Acetylene : IFuel
    {
        public int CarbonAmount => 2;

        public int HydrogenAmount => 2;

        public double Enthalpy => 226.75;

        public override string ToString()
        {
            return "Acetylene (C2H2)";
        }
    }
}
