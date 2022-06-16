using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Deton
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

            string path = "../save/autosave1.txt";
            string format = ".txt";
            int i = 2;

            while (File.Exists(path))
            {
                path = path.Remove(path.Length - format.Length  - (i % 1000).ToString().Length) + i.ToString() + format;
                i++;
            }

            using StreamWriter streamWriter = new StreamWriter(path);
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

            if (File.Exists(path))
            {
                File.Move(path, path.Replace(".txt", ".csv"));
            }
        }
    }
}