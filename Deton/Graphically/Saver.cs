using System.Collections.Generic;
using System.IO;

namespace Deton.Graphically
{
    internal class Saver
    {
        public static void Autosave(List<double[]> functionsPoints)
        {
            if (!File.Exists("../save"))
            {
                //File.Create("../save");
                Directory.CreateDirectory("../save");
            }


            string path = "../save/autosave999.csv";
            string previoslyPath = "../save/autosave998.csv"; ;
            string csvFormat = ".csv";
            int i = 998;

            while (!File.Exists(previoslyPath))
            {
                path = previoslyPath;

                previoslyPath = previoslyPath.Remove(previoslyPath.Length - csvFormat.Length  - (i % 1000).ToString().Length);

                i--;

                previoslyPath = previoslyPath + i.ToString() + csvFormat;
            }

            string txtFormat = ".txt";
            string path2 = path.Remove(path.Length - csvFormat.Length) + txtFormat;

            using StreamWriter streamWriter = new StreamWriter(path2);
            {
                for (int j = 0; j < functionsPoints[j].Length ; j++)
                {
                    for (int k = 0; k < functionsPoints.Count; k++)
                    {
                        streamWriter.WriteLine(functionsPoints[k][j]);
                    }

                    streamWriter.WriteLine();
                    streamWriter.WriteLine();
                }
            }

            streamWriter.Dispose();

            if (File.Exists(path2))
            {
                File.Move(path2, path2.Replace(txtFormat, csvFormat));
            }
        }
    }
}