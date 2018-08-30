using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace PlateSolveWrapper
{
    public class PlateSolver
    {
        public Coordinate PlateSolve(string fileName, double ra, double dec, double fieldWidth, double fieldHeight, int maxTiles, string solverPath)
        {
            Coordinate coordinates = null;

            try
            {
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
                proc.Start();
                proc.WaitForExit();

                string apmFileName = Path.Combine(
                    Path.GetDirectoryName(fileName),
                    Path.ChangeExtension(Path.GetFileNameWithoutExtension(fileName), "apm")
                );

                coordinates = ReadApmFile(apmFileName);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to start plate solver. Please check solver location.", ex);
            }

            return coordinates;
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
