using OTelescope.SampleAPI;
using System;
using System.Threading;

namespace PlateSolveWrapper
{
    public class BackyardEosCamera 
    {
        private bool _waitingForImage = false;
        private int _port;
        private DateTime _exposureStartTime;
        private const int timeout = 60;
        private double _lastDuration;
        private string _lastFileName;

        public BackyardEosCamera(int port)
        {
            _port = port;
            _backyardTcpClient = new OTelescopeTcpClient(_port);
        }

        private OTelescopeTcpClient _backyardTcpClient;

        public event EventHandler<ImageReadyEventArgs> ImageReady;
        public event EventHandler<ExposureFailedEventArgs> ExposureFailed;

        public string Model
        {
            get
            {
                return _backyardTcpClient.SendCommand("getcameramodel");
            }
        }
        
        public void AbortExposure()
        {
            _backyardTcpClient.SendCommand("abort");
        }

        public void ConnectCamera()
        {

        }

        public void DisconnectCamera()
        {
        }

        public void Dispose()
        {
            _backyardTcpClient?.Dispose();
        }

        public void StartExposure(double Duration, bool Light, int iso, bool isRaw)
        {
            string quality = GetQualityStr(isRaw);
            var command = string.Format("takepicture quality:{0} duration:{1} iso:{2} bin:1", quality, Duration, iso);
            _backyardTcpClient.SendCommand(command);

            MarkWaitingForExposure(Duration);

            ThreadPool.QueueUserWorkItem(state =>
            {
                CheckDownload();
            });
        }

        private string GetQualityStr(bool isRaw)
        {
            if (isRaw)
            {
                return "raw";
            }
            else
            {
                return "jpg";
            }
        }

        private void MarkWaitingForExposure(double Duration)
        {
            _exposureStartTime = DateTime.Now;
            _lastDuration = Duration;
            _waitingForImage = true;
        }

        private bool IsTimeout(string status)
        {
            var timeElapsed = DateTime.Now - _exposureStartTime;
            bool isTimeout = status == "busy" && timeElapsed.TotalSeconds > _lastDuration + timeout;

            return isTimeout;
        }

        private bool CheckStatus()
        {
            bool isOk = true;
            var status = _backyardTcpClient.SendCommand("getstatus");
            if (status == "error")
            {                
                CallExposureFailed("CameraError");
                isOk = false;
            }
            else if (IsTimeout(status))
            {
                CallExposureFailed("Connection timeout");
                isOk = false;
            }

            return isOk;
        }

        private bool TryDownload()
        {
            bool downloaded = false;
            var readyStr = _backyardTcpClient.SendCommand("getispictureready");
            bool ready = readyStr.Equals(bool.TrueString);
            if (ready)
            {
                var filepath = _backyardTcpClient.SendCommand("getpicturepath").Trim();

                if (ImageReady != null && _waitingForImage && !string.IsNullOrEmpty(filepath) && filepath != _lastFileName)
                {
                    ImageReady(this, new ImageReadyEventArgs(filepath));
                    _lastFileName = filepath;
                    _waitingForImage = false;
                    downloaded = true;
                }
            }

            return downloaded;
        }
        

        private void CheckDownload()
        {            
            while (true)
            {
                try
                {
                    if (!CheckStatus() || TryDownload())
                    {
                        break;
                    }

                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    CallExposureFailed(e.Message, e.StackTrace);
                    break;
                }
            }
        }

        private void CallExposureFailed(string message, string stackTrace = null)
        {
            _waitingForImage = false;
            ExposureFailed?.Invoke(this, new ExposureFailedEventArgs(message, stackTrace));
        }

        public void StopExposure()
        {
            AbortExposure();
        }
    }


    public class ImageReadyEventArgs : EventArgs
    {
        public ImageReadyEventArgs(string fileName)
        {
            RawFileName = fileName;
        }
        public string RawFileName { get; private set; }
    }

    public class ExposureFailedEventArgs : EventArgs
    {
        public ExposureFailedEventArgs(string message, string stacktrace = null)
        {
            Message = message;
            StackTrace = stacktrace;
        }
        public string Message { get; private set; }
        public string StackTrace { get; private set; }
    }
}
