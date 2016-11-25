using System.Windows.Media.Imaging;
using PropertyChanged;

namespace RustBotCSharp.GUI
{
    [ImplementPropertyChanged]
    public class SEVDataModel
    {
        public WriteableBitmap LeftImageWriteableBitmap { get; set; }
        public WriteableBitmap RightImageWriteableBitmap { get; set; }
        public CameraPoseModel LeftCameraPoseModel { get; set; } = new CameraPoseModel();
        public CameraPoseModel RightCameraPoseModel { get; set; } = new CameraPoseModel();
        public GNSSModel GNSSModel { get; set; } = new GNSSModel();
    }
}
