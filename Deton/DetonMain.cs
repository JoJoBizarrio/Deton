using Deton.Fuels;

namespace Deton
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

            IFuel[] fuels = new IFuel[] {new NotSelected(), new Hydrogen(), new Acetylene()}; 

            Application.Run(new DetonWiew(fuels));
        }
    }
}