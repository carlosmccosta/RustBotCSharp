using System;
using System.Windows;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf.SharpDX;
using Renci.SshNet;
using SharpDX;

namespace RustBotCSharp.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SEVDataSubscriberWPF SEVDataSubscriberWPF { get; set; } = new SEVDataSubscriberWPF();
        private SshClient SSHClient = null;
        private bool _streamingActive = false;
        private bool _recordActive = false;
        private bool _playbackActive = false;
        private const string _activeStreamingText = "Stop streaming";
        private const string _inactiveStreamingText = "Start streaming";
        private const string _activeRecordingText = "Stop recording";
        private const string _inactiveRecordingText = "Start recording";
        private const string _activePlaybackText = "Stop playback";
        private const string _inactivePlaybackText = "Start playback";
        private const string _encryptionKey = "3101337073";

        public MainWindow()
        {
            InitializeComponent();
            InitializeViewport3D();
            DataContext = SEVDataSubscriberWPF.SEVDataModel;
            SSHPasswordBox.Password = Encryption.DecryptString(SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Password, _encryptionKey);
        }

        private void InitializeViewport3D()
        {
            Viewport3DXGrid.Geometry = LineBuilder.GenerateGrid(SharpDX.Vector3.UnitY, -20, 20);
            Viewport3DXGrid.Transform = new TranslateTransform3D(0, 1.7, 0);
            Viewport3DXGrid.Color = Color.DarkGray;
        }

        public bool InitializeStreaming()
        {
            if (
                SEVDataSubscriberWPF.InitializeSubscriber(
                    SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.StreamingPublisherCommunicationsModel.URL(),
                    SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.StreamingPublisherTopic))
            {
                SEVDataSubscriberWPF.StartReceivingDataAsynchronously();
                _streamingActive = true;
                StreamingButton.Content = _activeStreamingText;
                return true;
            }
            return false;
        }

        public bool StopStreaming()
        {
            if (_streamingActive)
            {
                SEVDataSubscriberWPF.StopReceivingData();
                _streamingActive = false;
                StreamingButton.Content = _inactiveStreamingText;
                return true;
            }
            return false;
        }

        public int ExecuteSSHCommand(string command)
        {
            try
            {
                if (SSHClient != null && SSHClient.IsConnected)
                {
                    SshCommand x = SSHClient.RunCommand(command);
                    return x.ExitStatus;
                }
            }
            catch (Exception)
            {
                return -1;
            }
            return -1;
        }

        public void InitializeSSHClient()
        {
            SSHClient = new SshClient(
                SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Host,
                SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Port,
                SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Username,
                Encryption.DecryptString(SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Password, _encryptionKey));
            SSHClient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(1.0);
            SSHClient.Connect();
        }

        public bool InitializeRecording()
        {
            SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Status = "Initializing recording...";
            try
            {
                if (SSHClient == null)
                    InitializeSSHClient();
                else if (!SSHClient.IsConnected)
                    SSHClient.Connect();

                int exitStatus = ExecuteSSHCommand(SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHStartRecordCommand);
                if (exitStatus == 0 || exitStatus == 1)
                {
                    _recordActive = true;
                    RecordingButton.Content = _activeRecordingText;
                    SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Status = "Recording started.";
                    return true;
                }

                SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Status = "Failed to run start recording script with exit code " + exitStatus;
                return false;
            }
            catch (Exception)
            {
                SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Status = "Failed to connect to SSH server...";
                return false;
            }
        }

        public bool StopRecording()
        {
            SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Status = "Stopping recording...";
            int exitStatus = ExecuteSSHCommand(SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHStopRecordCommand);
            if (exitStatus == 0 || exitStatus == 1)
            {
                _recordActive = false;
                RecordingButton.Content = _inactiveRecordingText;
                SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Status = "Recording stopped.";
                return true;
            }
            SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Status = "Failed to stop recording with SSH exit code " + exitStatus;
            return false;
        }

        public bool InitializePlayback()
        {
            SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Status = "Initializing playback...";
            try
            {
                if (SSHClient == null)
                    InitializeSSHClient();
                else if (!SSHClient.IsConnected)
                    SSHClient.Connect();

                int exitStatus = ExecuteSSHCommand(SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHStartPlaybackCommand);
                if (exitStatus == 0 || exitStatus == 1)
                {
                    _playbackActive = true;
                    PlaybackButton.Content = _activePlaybackText;
                    SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Status = "Playback started.";
                    return true;
                }
                SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Status = "Failed to start playback with SSH exit code " + exitStatus;
                return false;
            }
            catch (Exception)
            {
                SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Status = "Failed to connect to SSH server...";
                return false;
            }
        }
        public bool StopPlayback()
        {
            SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Status = "Stopping playback...";
            int exitStatus = ExecuteSSHCommand(SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHStopPlaybackCommand);
            if (exitStatus == 0 || exitStatus == 1)
            {
                _playbackActive = false;
                PlaybackButton.Content = _inactivePlaybackText;
                SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Status = "Playback stopped.";
                return true;
            }
            SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Status = "Failed to stop playback with SSH exit code " + exitStatus;
            return false;
        }

        private void StreamingButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_streamingActive)
                StopStreaming();
            else
                InitializeStreaming();
        }

        private void RecordingButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_recordActive)
                StopRecording();
            else
                InitializeRecording();
        }

        private void PlaybackButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_playbackActive)
                StopPlayback();
            else
                InitializePlayback();
        }

        private void SSHPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Password = Encryption.EncryptString(SSHPasswordBox.Password, _encryptionKey);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                //NetMQConfig.Cleanup(false);
                if (SSHClient != null)
                {
                    SSHClient.Disconnect();
                    SSHClient.Dispose();
                }
            }
            catch (Exception)
            {}

            base.OnClosing(e);
            Application.Current.Shutdown();
            Environment.Exit(0);
        }
    }
}
