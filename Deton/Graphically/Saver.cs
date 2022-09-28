using System.Collections.Generic;
using System.IO;
using System;
using Deton.Logic;

namespace Deton.Graphically
{
    internal class Saver 
    {
        public static void Autosave(List<double[]> functionsPoints, Conditions detonationConditions)
        {
            string folderPath = Environment.CurrentDirectory + "\\Autosave";

            if (!File.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string csvPath = folderPath + "\\autosave999.csv";
            string previoslyCsvPath = folderPath + "\\autosave998.csv";
            string csvFormat = ".csv";
            int i = 998;

            while (i != 0 && !File.Exists(previoslyCsvPath))
            {
                csvPath = previoslyCsvPath;
                previoslyCsvPath = previoslyCsvPath.Remove(previoslyCsvPath.Length - csvFormat.Length - (i % 1000).ToString().Length);

                i--;

                previoslyCsvPath = previoslyCsvPath + i.ToString() + csvFormat;
            }

            string txtFormat = ".txt";
            string txtPath = csvPath.Remove(csvPath.Length - csvFormat.Length) + txtFormat;

            string[] valuesTitles = new string[] 
            {
                "1 - Detonation velocity (D), m/s", "2 - Tempreture (T), K", "3 - Pressure (P), atm", "4 - Density (R0), kg/m^3", 
                "5 - Mass velocity (U), m/s", "6 - Molecular mass (W), g/mol", "7 - Kinetic head (Ro*U^2/2), atm", 
                "8 - O", "9 - O2", "10 - H", "11 - H2", "12 - OH", "13 - H2O", 
                "14 - CO", "15 - CO2", "16 - N2", "17 - NO", "18 - Ar"
            };

            using StreamWriter streamWriter = new StreamWriter(txtPath);
            {
                streamWriter.WriteLine(detonationConditions);

                for (int j = 0; j < functionsPoints[j].Length; j++)
                {
                    streamWriter.Write(valuesTitles[j]);
                    streamWriter.Write(";");

                    for (int k = 0; k < functionsPoints.Count; k++)
                    {
                        streamWriter.Write(functionsPoints[k][j]);
                        streamWriter.Write(";");
                    }

                    streamWriter.WriteLine();
                }
            }

            streamWriter.Dispose();

            if (File.Exists(txtPath))
            {
                    File.Move(txtPath, txtPath.Replace(txtFormat, csvFormat), true);
            }
        }
    }
}