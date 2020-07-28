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
        private CancellationTokenSource tokenSource;

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
          
            if (_settings.AutoConnect)
            {
                ConnectTelescope(_settings.LastTelescopeId);
                ConnectCamera(_settings.LastCameraId);
            }
            UpdateUiState();
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
            cbAutoConnect.Checked = _settings.AutoConnect;
        }

        private void ReadSettingsFromUi()
        {
            _settings.FieldWidth = (int)numFieldWidth.Value;
            _settings.FieldHeight = (int)numFieldHeight.Value;
            _settings.SolverPath = tbPlateSolverPath.Text;
            _settings.SearchTiles = (int)numSearchTiles.Value;
            _settings.Exposure = (int)numExposure.Value;
            _settings.AutoConnect = cbAutoConnect.Checked;
        }

        private void btnConnectMount_Click(object sender, EventArgs e)
        {
            if (!IsScopeConnected())
            {
                if (!ConnectTelescope(_settings.LastTelescopeId))
                {
                    MessageBox.Show("Failed to connect telescope");                    
                }
            }
            else
            {
                DisconnectTelescope();
            }

            UpdateUiState();
        }

        private void SolveAndSync(string fileName, CancellationToken token)
        {
            _settings.LastImagePath = Path.GetFullPath(fileName);

            var coordinate = _plateSolver.StartPlateSolve(fileName,
                                                _telescope.RightAscension,
                                                _telescope.Declination,
                                                _settings.FieldWidth,
                                                _settings.FieldHeight,
                                                _settings.SearchTiles,
                                                _settings.SolverPath, token);

            isProcessing = false;
            if (coordinate != null)
            {
                _telescope.SyncToCoordinates(coordinate.Ra, coordinate.Dec);
                _synchronizationContext.Post(o =>
                {
                    tbSolvedCoordinates.Text = string.Format("{0}, Angle: {1:0.##}", GetCoordinatesStr(coordinate.Ra, coordinate.Dec), coordinate.CameraAngle);
                    lblSolvedFileName.Text = Path.GetFileName(fileName);
                    Update("Solve successful");
                }, null);
            }
            else
            {
                _synchronizationContext.Post(o =>
                {
                    tbSolvedCoordinates.Text = string.Empty;
                    lblSolvedFileName.Text = string.Empty;
                    Update("Solve failed");
                }, null);
            }

        }

        private void ClearResult()
        {
            _synchronizationContext.Post(o =>
            {
                lblSolvedFileName.Text = string.Empty;
                tbSolvedCoordinates.Text = "";
            }, null);
        }

        public bool ConnectTelescope(string progID)
        {
            bool isConnected = false;
            try
            {
                _telescope = new Telescope(progID);
                _telescope.Connected = true;
                _settings.LastTelescopeId = progID;
                isConnected = true;
            }
            catch (Exception)
            {
                _settings.LastTelescopeId = string.Empty;
            }

            return isConnected;
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

        private void btnSelectTelescope_Click(object sender, EventArgs e)
        {
            try
            {
                string progID = Telescope.Choose(_settings.LastTelescopeId);
                _settings.LastTelescopeId = progID;
                ConnectTelescope(progID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
            btnConnectMount.Text = scopeConnected ? "Disconnect" : "Connect";
            btnSelectTelescope.Enabled = !scopeConnected;
            btnOpenSolveAndSync.Enabled = scopeConnected;
            btnConnectMount.Enabled = !string.IsNullOrEmpty(_settings.LastTelescopeId);

            bool cameraConnected = IsCameraConnected();
            btnSelectCamera.Enabled = !cameraConnected;
            btnConnectCamera.Text = cameraConnected ? "Disconnect" : "Connect";
            btnShotSolveSync.Enabled = scopeConnected && cameraConnected && !isProcessing;
            btnConnectCamera.Enabled = !string.IsNullOrEmpty(_settings.LastCameraId);

            bool isIdle = cameraConnected && _camera.CameraState == ASCOM.DeviceInterface.CameraStates.cameraIdle;
            btnShotSolveSync.Enabled = scopeConnected && isIdle && !isProcessing;
            btnOpenSolveAndSync.Enabled = !isProcessing;

            if (!string.IsNullOrEmpty(progressMessage))
            {
                lblStatus.Text = progressMessage;
            }

            btnAbort.Enabled = isProcessing;

            gbScope.Enabled = !isProcessing;
            gbCamera.Enabled = !isProcessing;
            gbSettings.Enabled = !isProcessing;

            lblScopeName.Text = _settings.LastTelescopeId;
            lblCameraName.Text = _settings.LastCameraId;
        }

        private string GetCoordinatesStr(double ra, double dec)
        {
            var raAngle = _util.HoursToHMS(ra);
            var decAngle = _util.DegreesToDMS(dec);
            string decSign = dec >= 0 ? "+" : "";
            string result = string.Format("RA: {0}, DEC: {1}{2}", raAngle.ToString(), decSign, decAngle.ToString());

            return result;
        }

        private void btnConnectCamera_Click(object sender, EventArgs e)
        {
            if (!IsCameraConnected())
            {
                if (!ConnectCamera(_settings.LastCameraId))
                {                    
                    MessageBox.Show("Failed to connect camera");
                }
            }
            else
            {
                DisconnectCamera();
            }
            UpdateUiState();
        }

        private void btnSelectCamera_Click(object sender, EventArgs e)
        {
            string progID = Camera.Choose(_settings.LastCameraId);
            _settings.LastCameraId = progID;
            ConnectCamera(progID);
            UpdateUiState();
        }

        private bool ConnectCamera(string progID)
        {
            bool isConnected = false;
            try
            {
                _camera = new Camera(progID);
                _camera.Connected = true;
                _settings.LastCameraId = progID;
                isConnected = true;
            }
            catch (Exception)
            {
                _settings.LastCameraId = string.Empty;
            }

            return isConnected;
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
                    isProcessing = true;
                    ClearResult();
                    tokenSource = new CancellationTokenSource();
                    var token = tokenSource.Token;
                    SolveAndSync(dialog.FileName, tokenSource.Token);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isProcessing = false;
            }
        }

        private async void btnShotSolveSync_Click(object sender, EventArgs e)
        {
            tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            await ShotSolveSync(token);
        }

        private async Task ShotSolveSync(CancellationToken token)
        {
            
            await Task.Run(() =>
            {
              
                try
                {
                    ClearResult();
                    isProcessing = true;
                    ReadSettingsFromUi();
                    string fileName = Path.Combine(
                        System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                        "temp.jpg");
                    _camera.StartExposure(_settings.Exposure, true);
                    Update("Exposure...");
                    while (!_camera.ImageReady || _camera.CameraState != ASCOM.DeviceInterface.CameraStates.cameraIdle)
                    {
                        _util.WaitForMilliseconds(300);
                        token.ThrowIfCancellationRequested();
                    }                    
                    Update("Reading image...");

                    var array = (Array)_camera.ImageArray;
                    var bmp = ImageHelper.GetBitmap(array, _camera.MaxADU);
                    ImageHelper.SaveBmp(bmp, fileName);
                    Update("Solving...");
                    SolveAndSync(fileName, token);                    
                }
                catch (OperationCanceledException)
                {
                    Update("Aborted");
                    if (_camera.CameraState == ASCOM.DeviceInterface.CameraStates.cameraExposing)
                    {
                        _camera.AbortExposure();
                    }
                    isProcessing = false;
                }
                catch (Exception ex)
                {
                    isProcessing = false;
                    Update();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }, token);
        }

        private void Update(string status = " ")
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

     
        private bool isProcessing;

        private void btnAbort_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
        }
    }
}
