using System;
using System.Windows;
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
        private const string _activeStreamingText = "Stop Streaming";
        private const string _inactiveStreamingText = "Start Streaming";
        private const string _activeRecordingText = "Stop Recording";
        private const string _inactiveRecordingText = "Start Recording";

        public MainWindow()
        {
            InitializeComponent();
            InitializeViewport3D();
            DataContext = SEVDataSubscriberWPF.SEVDataModel;
        }

        private void InitializeViewport3D()
        {
            Viewport3DXGrid.Geometry = LineBuilder.GenerateGrid(Vector3.UnitX, -10, 10);
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
                SEVDataSubscriberWPF.StopReceivingDataAsynchronously();
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
            catch (Exception e)
            {
                return -1;
            }
            return -1;
        }

        public bool InitializeRecording()
        {
            try
            {
                SSHClient = new SshClient(
                    SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Host,
                    SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Port,
                    SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHConnectionModel.Username,
                    SSHPasswordBox.Password);
                SSHClient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(1.0);
                SSHClient.Connect();
                if (ExecuteSSHCommand(SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHStartRecordCommand) == 0)
                {
                    _recordActive = true;
                    RecordingButton.Content = _activeRecordingText;
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public bool StopRecording()
        {
            int exitStatus = ExecuteSSHCommand(SEVDataSubscriberWPF.SEVDataModel.CommunicationsModel.SSHStopRecordCommand);
            if (exitStatus == 0 || exitStatus == 1)
            {
                _recordActive = false;
                RecordingButton.Content = _inactiveRecordingText;
                SSHClient.Disconnect();
                SSHClient.Dispose();
                return true;
            }
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

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            //NetMQConfig.Cleanup(false);
            base.OnClosing(e);
            Application.Current.Shutdown();
            Environment.Exit(0);
        }
    }
}
