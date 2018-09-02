using System;

namespace PlateSolveWrapper
{
    [Serializable]
    public class Settings
    {
        public Settings()
        {
            FieldWidth = 76;
            FieldHeight = 50;
            SearchTiles = 300;
            Exposure = 10;
        }

        public int FieldWidth { get; set; }
        public int FieldHeight { get; set; }
        public string SolverPath { get; set; }
        public int SearchTiles { get; set; }
        public string LastImagePath { get; set; }
        public string LastTelescopeId { get; set; }
        public string LastCameraId { get; set; }
        public int Exposure { get; set; }
    }
}
