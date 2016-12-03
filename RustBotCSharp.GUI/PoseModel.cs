using PropertyChanged;

namespace RustBotCSharp.GUI
{
    [ImplementPropertyChanged]
    public class PoseModel
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Heading { get; set; }
        public double Attitude { get; set; }
        public double Bank { get; set; }
    }
}
