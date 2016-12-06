using PropertyChanged;

namespace RustBotCSharp.GUI
{
    [ImplementPropertyChanged]
    public class DiagnosticsModel
    {
        public int MessageSizeInBytes { get; set; }
        public double MessageNetworkReceiveTimeMilliseconds { get; set; }
        public double MessageParsingTimeMilliseconds { get; set; }
        public double MessageProcessingTimeMilliseconds { get; set; }
    }
}
