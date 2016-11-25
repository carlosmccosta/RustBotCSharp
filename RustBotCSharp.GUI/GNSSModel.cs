using PropertyChanged;

namespace RustBotCSharp.GUI
{
    [ImplementPropertyChanged]
    public class GNSSModel
    {
        public string Status { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
    }
}
