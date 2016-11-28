using PropertyChanged;

namespace RustBotCSharp.GUI
{
    [ImplementPropertyChanged]
    public class DiagnosticsModel
    {
        public int MessageSizeInBytes { get; set; }
        public long MessageParsingTimeMilliseconds { get; set; }
        public long MessageProcessingTimeMilliseconds { get; set; }
    }
}
