using System.Windows;
using RustBotCSharp.Communication;

namespace RustBotCSharp.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SEVDataSubscriberWPF SEVDataSubscriberWPF { get; set; } = new SEVDataSubscriberWPF();
        private bool _streamingActive = false;
        private const string _activeStreamingText = "Stop Streaming";
        private const string _inactiveStreamingText = "Start Streaming";
        public MainWindow()
        {
            InitializeComponent();
            DataContext = SEVDataSubscriberWPF.SEVDataModel;
        }

        private void InitializeStreaming()
        {
            SEVDataSubscriberWPF.InitializeSubscriber(SubscriberURLTextBox.Text, SubscriberTopicTextBox.Text);
            SEVDataSubscriberWPF.StartReceivingDataAsynchronously();
            _streamingActive = true;
        }

        private bool StopStreaming()
        {
            if (_streamingActive)
            {
                SEVDataSubscriberWPF.StopReceivingDataAsynchronously();
                _streamingActive = false;
                return true;
            }
            return false;
        }

        private void StreamingButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_streamingActive)
            {
                if (StopStreaming())
                    StreamingButton.Content = _inactiveStreamingText;
            }
            else
            {
                InitializeStreaming();
                StreamingButton.Content = _activeStreamingText;
            }
        }

        private void RecordingButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
