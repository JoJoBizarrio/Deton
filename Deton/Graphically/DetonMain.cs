using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            IFuel[] fuels = new IFuel[] 
            { 
                new NotSelected(), 
                new Hydrogen(),
                new Methane(),
                new Ethane(),
                new Ethylene(),
                new Acetylene(),
                new Propane(),
                new Propylene(),
                new Butane(), 
                new Butene(), 
                new Pentane() 
            };

            Application.Run(new DetonWiew(fuels));
        }
    }
}