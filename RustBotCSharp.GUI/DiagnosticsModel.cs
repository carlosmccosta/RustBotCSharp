using PropertyChanged;

namespace RustBotCSharp.GUI
{
    [ImplementPropertyChanged]
    public class DiagnosticsModel
    {
        public long MessageParsingTimeMilliseconds { get; set; }
        public long MessageProcessingTimeMilliseconds { get; set; }
    }
}
