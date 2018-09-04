using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace PlateSolveWrapper
{
    public class PlateSolver
    {
        private string _fileName;
        public void StartPlateSolve(string fileName, double ra, double dec, double fieldWidth, double fieldHeight, int maxTiles, string solverPath)
        {
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
                proc.EnableRaisingEvents = true;
                proc.Exited += Proc_Exited;
                _fileName = fileName;
                proc.Start();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to start plate solver. Please check solver location.", ex);
            }
        }

        private void Proc_Exited(object sender, EventArgs e)
        {
            string apmFileName = Path.Combine(
                    Path.GetDirectoryName(_fileName),
                    Path.ChangeExtension(Path.GetFileNameWithoutExtension(_fileName), "apm")
                );
             var coordinates = ReadApmFile(apmFileName);
            RaisePlateSolveFinished(coordinates, _fileName);
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

        public delegate void PlateSolveEventHandler(object sender, PlateSolveEventArgs e);

        public event PlateSolveEventHandler PlateSolveFinished;
        
        protected virtual void RaisePlateSolveFinished(Coordinate coordinate, string fileName)
        {
            // Raise the event by using the () operator.
            if (PlateSolveFinished != null)
                PlateSolveFinished(this, new PlateSolveEventArgs(coordinate, fileName));
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

    public class PlateSolveEventArgs
    {
        public PlateSolveEventArgs(Coordinate c, string fileName)
        {
            Coordinate = c;
            FileName = fileName;
        }
        public Coordinate Coordinate { get; private set; }
        public string FileName { get; private set; }
    }
}
