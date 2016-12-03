using System;
using System.Windows.Media.Imaging;
using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Core;
using SharpDX;
using PropertyChanged;

namespace RustBotCSharp.GUI
{
    [ImplementPropertyChanged]
    public class SEVDataModel
    {
        public SEVDataModel()
        {
            PointGeometry3D = new PointGeometry3D
            {
                Indices = new IntCollection(),
                Positions = new Vector3Collection(),
                Colors = new Color4Collection()
            };
        }

        public WriteableBitmap LeftImageWriteableBitmap { get; set; }
        public WriteableBitmap RightImageWriteableBitmap { get; set; }
        public PoseModel StereoSystemPoseModel { get; set; } = new PoseModel();
        public GNSSModel GNSSModel { get; set; } = new GNSSModel();
        public PointGeometry3D PointGeometry3D { get; set; }
        public CommunicationsModel CommunicationsModel { get; set; } = new CommunicationsModel("CommunicationsModel.xml");
        public DiagnosticsModel DiagnosticsModel { get; set; } = new DiagnosticsModel();
    }
}
