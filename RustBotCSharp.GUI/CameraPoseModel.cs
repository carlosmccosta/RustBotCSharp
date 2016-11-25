using PropertyChanged;

namespace RustBotCSharp.GUI
{
    [ImplementPropertyChanged]
    public class CameraPoseModel
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Roll { get; set; }
        public double Pitch { get; set; }
        public double Yaw { get; set; }
    }
}
