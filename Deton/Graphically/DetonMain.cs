using System;
using System.Windows.Forms;

using Deton.Fuels;

namespace Deton.Graphically
{
    internal static class DetonMain
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //ApplicationConfiguration.Initialize();

            IFuel[] fuels = new IFuel[] 
            { 
                new NotSelected(), 
                new Hydrogen(),
                new Methane(),
                new Acetylene(),
                new Ethylene(),
                new Ethane(),
                new Propylene(),
                new Propane(),
                new Butene(),
                new Butane(), 
                new Pentane() 
            };

            Application.Run(new DetonWiew(fuels));
        }
    }
}