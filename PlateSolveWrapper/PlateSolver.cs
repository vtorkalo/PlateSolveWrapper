using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace PlateSolveWrapper
{
    public class PlateSolver
    {
        private string _fileName;
        public Coordinate StartPlateSolve(string fileName, double ra, double dec, double fieldWidth, double fieldHeight, int maxTiles, string solverPath, CancellationToken cancellationToken)
        {
            Coordinate coordinate = null;

            var proc = new System.Diagnostics.Process();

            proc.StartInfo.FileName = solverPath;
            proc.StartInfo.Arguments =
                MathHelpers.HoursToRad(ra).ToString("0.00000", CultureInfo.InvariantCulture) + "," +           // ra, радиан
                MathHelpers.DegToRad(dec).ToString("0.00000", CultureInfo.InvariantCulture) + "," + // dec, радиан
                MathHelpers.DegToRad(fieldWidth / 60d).ToString("0.000", CultureInfo.InvariantCulture) + "," +                      // ширина поля, радиан
                MathHelpers.DegToRad(fieldHeight / 60d).ToString("0.000", CultureInfo.InvariantCulture) + "," +                     // высота поля, радиан
                maxTiles.ToString() + "," +                                                                                      // кол-во элемнетов спирали
                fileName + "," +                                                                                // имя фита
                "0";
            _fileName = fileName;
            proc.Start();
            
            while (!proc.HasExited)
            {
                Thread.Sleep(1000);
                if (cancellationToken.IsCancellationRequested)
                {
                    proc.Kill();
                }
                cancellationToken.ThrowIfCancellationRequested();
            }

            string apmFileName = Path.Combine(
             Path.GetDirectoryName(_fileName),
             Path.ChangeExtension(Path.GetFileNameWithoutExtension(_fileName), "apm")
                                             );
            coordinate = ReadApmFile(apmFileName);



            return coordinate;
        }



        private Coordinate ReadApmFile(string fileName)
        {
            Coordinate result = null;

            try
            {
                var lines = File.ReadAllLines(fileName);
                if (lines.Count() >= 3 && lines[2].StartsWith("Valid"))
                {
                    string[] firstLine = HandleSeparators(lines[0]).Split(',');
                    string[] secondLine = HandleSeparators(lines[0]).Split(',');
                    result = new Coordinate
                    {
                        Ra = MathHelpers.RadToHours(double.Parse(firstLine[0], CultureInfo.InvariantCulture)),
                        Dec = MathHelpers.RadToDeg(double.Parse(firstLine[1], CultureInfo.InvariantCulture)),
                        PixelScale = double.Parse(secondLine[0], CultureInfo.InvariantCulture),
                        CameraAngle = double.Parse(secondLine[1], CultureInfo.InvariantCulture),
                    };
                }
            }
            catch (Exception)
            {

            }

            return result;
        }



        private string HandleSeparators(string input)
        {
            //Handle case when system decimal separator is , and output looks like this:
            //0,88122,359,13,-1,00013,-0,00017,404
            string result = null;
            if (!input.Contains("."))
            {
                var split = input.Split(',');
                for (int i = 0; i < split.Count(); i++)
                {
                    if (i == split.Count() - 1)
                    {
                        result += split[i];
                    }
                    else if (i % 2 == 0)
                    {
                        result += split[i] + ".";
                    }
                    else
                    {
                        result += split[i] + ",";
                    }
                }
            }
            else
            {
                result = input;
            }

            return result;
        }
    }


}
