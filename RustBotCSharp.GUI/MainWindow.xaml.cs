using System.Windows;
using RustBotCSharp.Communication;

namespace RustBotCSharp.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SEVDataSubscriber SEVDataSubscriber { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            InitializeStreaming();
        }

        private void InitializeStreaming()
        {
            SEVDataSubscriber = new SEVDataSubscriber();
        }
    }
}
