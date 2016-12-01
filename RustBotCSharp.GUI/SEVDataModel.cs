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

            Random rng = new Random();

            for (float x = 0; x < 5; x += 0.5f)
            {
                float start = -0.5f - x * 0.5f;
                float end = 0.5f + x * 0.5f;
                for (float y = start; y < end; y += 0.5f)
                {
                    for (float z = start; z < end; z += 0.5f)
                    {
                        PointGeometry3D.Indices.Add(PointGeometry3D.Positions.Count);
                        PointGeometry3D.Positions.Add(new Vector3(x, y, z));
                        PointGeometry3D.Colors.Add(new Color4(rng.NextFloat(0, 1), rng.NextFloat(0, 1), rng.NextFloat(0, 1), 1));
                    }
                }
            }
        }

        public WriteableBitmap LeftImageWriteableBitmap { get; set; }
        public WriteableBitmap RightImageWriteableBitmap { get; set; }
        public CameraPoseModel LeftCameraPoseModel { get; set; } = new CameraPoseModel();
        public CameraPoseModel RightCameraPoseModel { get; set; } = new CameraPoseModel();
        public GNSSModel GNSSModel { get; set; } = new GNSSModel();
        public PointGeometry3D PointGeometry3D { get; set; }
        public CommunicationsModel CommunicationsModel { get; set; } = new CommunicationsModel("CommunicationsModel.xml");
        public DiagnosticsModel DiagnosticsModel { get; set; } = new DiagnosticsModel();
    }
}
