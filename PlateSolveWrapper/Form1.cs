using ASCOM.DriverAccess;
using ASCOM.Utilities;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlateSolveWrapper
{
    public partial class fmMain : Form
    {
        private PlateSolver _plateSolver;
        private Telescope _telescope;
        private Camera _camera;
        private SettingsProvider _settingsProvider;
        private string settingsFileName = "settings.xml";
        private Settings _settings;
        private System.Windows.Forms.Timer _timer;
        private Util _util = new Util();
        private readonly SynchronizationContext _synchronizationContext;
        private bool _aborted = false;
        public fmMain()
        {
            InitializeComponent();
            _synchronizationContext = SynchronizationContext.Current;
            _settingsProvider = new SettingsProvider(settingsFileName);
            _settings = _settingsProvider.ReadSettings();
            _timer = new System.Windows.Forms.Timer();
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
            numFieldWidth.Value = _settings.FieldWidth;
            numFieldHeight.Value = _settings.FieldHeight;
            tbPlateSolverPath.Text = _settings.SolverPath;
            numSearchTiles.Value = _settings.SearchTiles;
            numExposure.Value = _settings.Exposure;
        }

        private void ReadSettingsFromUi()
        {
            _settings.FieldWidth = (int)numFieldWidth.Value;
            _settings.FieldHeight =(int) numFieldHeight.Value;
            _settings.SolverPath = tbPlateSolverPath.Text;
            _settings.SearchTiles = (int)numSearchTiles.Value;
            _settings.Exposure = (int)numExposure.Value;
        }

        private void btnConnectMount_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectTelescope();                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect telescope");
            }
            UpdateUiState();
        }

        private void SolveAndSync(string fileName)
        {
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
                tbSolvedCoordinates.Text = string.Empty;
                lblSolvedFileName.Text = string.Empty;
                MessageBox.Show("Sync failed");
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
            ReadSettingsFromUi();
            _settingsProvider.SaveSettings(_settings);
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

        private bool IsCameraConnected()
        {
            bool connected = false;
            try
            {
                connected = _camera != null && _camera.Connected;
            }
            catch (Exception)
            {

            }
            return connected;
        }

        private void UpdateUiState(string progressMessage = "")
        {
            bool scopeConnected = IsScopeConnected();
            btnConnectMount.Enabled = !scopeConnected;
            btnDisconnect.Enabled = scopeConnected;
            btnOpenSolveAndSync.Enabled = scopeConnected;
            lblScopeName.Text = scopeConnected ? _telescope.Name : "Not connected";


            bool cameraConnected = IsCameraConnected();
            btnConnectCamera.Enabled = !cameraConnected;
            btnDisconnectCamera.Enabled = cameraConnected;
            lblCameraName.Text = cameraConnected ? _camera.Name : "Not connected";
            btnShotSolveSync.Enabled = scopeConnected && cameraConnected;


            bool isIdle = cameraConnected && _camera.CameraState == ASCOM.DeviceInterface.CameraStates.cameraIdle;
            btnShotSolveSync.Enabled = isIdle;
            bool isExposing = cameraConnected && _camera.CameraState == ASCOM.DeviceInterface.CameraStates.cameraExposing;

            btnOpenSolveAndSync.Enabled = !isExposing;

            if (!string.IsNullOrEmpty(progressMessage))
            {
                lblStatus.Text = progressMessage;
            }

            btnAbort.Enabled = _longRunningOperation;
        }

        private string GetCoordinatesStr(double ra, double dec)
        {
            var raAngle = _util.HoursToHMS(ra);
            var decAngle = _util.DegreesToDMS(dec);
            string decSign = dec >= 0? "+" : "";
            string result = string.Format("RA: {0}, DEC: {1}{2}", raAngle.ToString(), decSign, decAngle.ToString());

            return result;
        }

        private void btnConnectCamera_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectCamera();                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Failed to connect camera");
            }
            UpdateUiState();
        }

        private void btnDisconnectCamera_Click(object sender, EventArgs e)
        {
            DisconnectCamera();
            UpdateUiState();
        }

        private void ConnectCamera()
        {
            string progID = Camera.Choose(_settings.LastCameraId);
            if (!string.IsNullOrEmpty(progID))
            {
                _camera = new Camera(progID);
                _camera.Connected = true;
                _settings.LastCameraId = progID;
            }
        }

        private void DisconnectCamera()
        {
            _camera.Connected = false;
            _camera.Dispose();
        }

        private void btnOpenSolveSync_Click(object sender, EventArgs e)
        {
            try
            {
                ReadSettingsFromUi();
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Title = "Open Image";
                dialog.InitialDirectory = _settings.LastImagePath;
                dialog.Filter = "Images|*.jpg;*.jpeg;*.fit;*.fits;";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    SolveAndSync(dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnShotSolveSync_Click(object sender, EventArgs e)
        {
           await ShotSolveSync();
        }    

        private async Task ShotSolveSync()
        {
            await Task.Run(()=>{
                try
                {
                    _longRunningOperation = true;
                    ReadSettingsFromUi();
                    string fileName = Path.Combine(
                        System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                        "temp.jpg");
                    _camera.StartExposure(_settings.Exposure, true);
                    Update("Exposure...");
                    while (!_camera.ImageReady)
                    {
                       _util.WaitForMilliseconds(300);
                        CheckAbortState();
                    }

                    Update("Reading image...");
                    var array = (Array)_camera.ImageArray;
                    var bmp = ImageHelper.GetBitmap((Array)_camera.ImageArray, _camera.MaxADU);
                    ImageHelper.SaveBmp(bmp, fileName);
                    Update("Solving...");
                    SolveAndSync(fileName);
                }
                catch (OperationCanceledException)
                {
                    _aborted = false;
                    Update("Aborted");
                    if (_camera.CameraState == ASCOM.DeviceInterface.CameraStates.cameraExposing)
                    {
                        _camera.AbortExposure();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                _longRunningOperation = false;
            });
        }

        private void Update(string status)
        {
            _synchronizationContext.Post(c =>
            {
                string msg = c as string;
                UpdateUiState(msg);
            }, status);
        }

        public static void Do<TControl>(TControl control, Action<TControl> action) where TControl : Control
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action, control);
            }
            else
            {
                action(control);
            }
        }

        private void CheckAbortState()
        {
            if (_aborted)
            {
                throw new OperationCanceledException();
            }
        }
        private bool _longRunningOperation;

        private void btnAbort_Click(object sender, EventArgs e)
        {
            _aborted = true;
        }
    }
}
