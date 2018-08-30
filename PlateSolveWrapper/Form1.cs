using ASCOM.DriverAccess;
using System;
using System.IO;
using System.Windows.Forms;

namespace PlateSolveWrapper
{
    public partial class fmMain : Form
    {
        private PlateSolver _plateSolver;
        private Telescope _telescope;
        private SettingsProvider _settingsProvider;
        private string settingsFileName = "settings.xml";
        private Settings _settings;
        private System.Windows.Forms.Timer _timer;

        public fmMain()
        {
            InitializeComponent();
            _settingsProvider = new SettingsProvider(settingsFileName);
            _settings = _settingsProvider.ReadSettings();
            _timer = new Timer();
            _timer.Interval = 2000;
            _timer.Tick += _timer_Tick;
            _timer.Start();
            UnitSettingsUI();

            _plateSolver = new PlateSolver();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (IsScopeConnected())
            {
                tbCurrentCoordinates.Text = GetCoordinatesStr(_telescope.RightAscension, _telescope.Declination);
            }
            else
            {
                tbCurrentCoordinates.Text = string.Empty;
            }
            UpdateUiState();
        }

        private void UnitSettingsUI()
        {
            numFieldWidth.Value = (decimal)_settings.FieldWidth;
            numFieldHeight.Value = (decimal)_settings.FieldHeight;
            tbPlateSolverPath.Text = _settings.SolverPath;
            numSearchTiles.Value = _settings.SearchTiles;
        }

        private void btnConnectMount_Click(object sender, EventArgs e)
        {
            ConnectTelescope();
            UpdateUiState();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SolveAndSync();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SolveAndSync()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open Image";
            dialog.InitialDirectory = _settings.LastImagePath;
            dialog.Filter = "Images|*.jpg;*.jpeg;*.tiff;*.tif;*.fit;*.fits;";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                _settings.LastImagePath = Path.GetFullPath(fileName);

                var coords = _plateSolver.PlateSolve(fileName,
                                                    _telescope.RightAscension,
                                                    _telescope.Declination,
                                                    _settings.FieldWidth,
                                                    _settings.FieldHeight,
                                                    _settings.SearchTiles,
                                                    _settings.SolverPath);
                if (coords != null)
                {
                    _telescope.SyncToCoordinates(coords.Ra, coords.Dec);
                    tbSolvedCoordinates.Text = string.Format("{0}, Angle: {1:0.##}", GetCoordinatesStr(coords.Ra, coords.Dec), coords.CameraAngle);
                    lblSolvedFileName.Text = Path.GetFileName(fileName);
                    MessageBox.Show("Sync successful");
                }
                else
                {
                    MessageBox.Show("Sync failed");
                }
            }
        }

        public void ConnectTelescope()
        {
            string progID = Telescope.Choose(_settings.LastTelescopeId);
            if (!string.IsNullOrEmpty(progID))
            {
                _telescope = new Telescope(progID);
                _telescope.Connected = true;
                _telescope.Tracking = true;
                _settings.LastTelescopeId = progID;
            }
        }

        public void DisconnectTelescope()
        {
            _telescope.Connected = false;
            _telescope.Dispose();
        }

        public Settings Settings { get; set; }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _settingsProvider.SaveSettings(_settings);
        }

        private void numFieldWidth_ValueChanged(object sender, EventArgs e)
        {
            _settings.FieldWidth = (double)numFieldWidth.Value;
        }

        private void numFieldHeight_ValueChanged(object sender, EventArgs e)
        {
            _settings.FieldHeight = (double)numFieldHeight.Value;
        }

        private void btnBrowseSolver_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open PlateSolve2.exe";
            theDialog.InitialDirectory = @"C:\";
            theDialog.Filter = "PlateSolve2.exe|PlateSolve2.exe";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                _settings.SolverPath = theDialog.FileName;
            }
            tbPlateSolverPath.Text = _settings.SolverPath;
        }

        private void numSearchTiles_ValueChanged(object sender, EventArgs e)
        {
            _settings.SearchTiles =(int) numSearchTiles.Value;
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            DisconnectTelescope();
            UpdateUiState();
        }

        private bool IsScopeConnected()
        {
            bool connected = false;
            try
            {
                connected = _telescope != null && _telescope.Connected;
            }
            catch (Exception)
            {

            }
            return connected;
        }

        private void UpdateUiState()
        {
            bool scopeConnected = IsScopeConnected();
            btnConnectMount.Enabled =  !scopeConnected;
            btnDisconnect.Enabled = scopeConnected;
            btnSolveAndSync.Enabled = scopeConnected;

            if (scopeConnected)
            {
                lblScopeName.Text = _telescope.Name;
            }
            else
            {
                lblScopeName.Text = "Not connected";
            }
        }

        private string GetCoordinatesStr(double ra, double dec)
        {
            var raAngle = GeoAngle.FromDouble(ra);
            var decAngle = GeoAngle.FromDouble(dec);
            string decSign = decAngle.IsNegative ? "" : "+";
            string result = string.Format("RA: {0}, DEC: {1}{2}", raAngle.ToString(), decSign, decAngle.ToString());
            return result;
        }
    }
}
