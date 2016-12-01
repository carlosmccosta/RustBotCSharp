using System;
using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Core;
using SharpDX;

namespace RustBotCSharp.MessageConverter
{
    public class PointCloudConverter
    {
        private const int FloatDataType = 7;
        public static PointGeometry3D ConvertToPointGeometry3D(PointCloud2 pointcloud)
        {
            PointGeometry3D pointGeometry3D = new PointGeometry3D
            {
                Indices = new IntCollection(),
                Positions = new Vector3Collection(),
                Colors = new Color4Collection()
            };

            int xOffset = -1;
            int yOffset = -1;
            int zOffset = -1;
            int rgbOffset = -1;

            foreach (PointField field in pointcloud.Fields)
            {
                if (field.Name == "x" && field.Datatype == FloatDataType)
                    xOffset = field.Offset;
                else
                if (field.Name == "y" && field.Datatype == FloatDataType)
                    yOffset = field.Offset;
                else
                if (field.Name == "z" && field.Datatype == FloatDataType)
                    zOffset = field.Offset;
                else
                if (field.Name == "rgb" && field.Datatype == FloatDataType)
                    rgbOffset = field.Offset;
            }

            if (xOffset >= 0 && yOffset >= 0 && zOffset >= 0 && rgbOffset >= 0)
            {
                byte[] pointcloudData = pointcloud.Data.ToByteArray();
                if (pointcloud.IsBigendian)
                    Array.Reverse(pointcloudData);

                for (int i = 0; i < pointcloudData.Length; i += pointcloud.PointStep)
                {
                    float xPosition = BitConverter.ToSingle(pointcloudData, i + xOffset);
                    if (!float.IsNaN(xPosition))
                    {
                        float yPosition = BitConverter.ToSingle(pointcloudData, i + yOffset);
                        if (!float.IsNaN(xPosition))
                        {
                            float zPosition = BitConverter.ToSingle(pointcloudData, i + zOffset);
                            if (!float.IsNaN(xPosition))
                            {
                                Vector3 pointPosition = new Vector3(xPosition, yPosition, zPosition);

                                Color4 pointColor = new Color4(
                                (float)pointcloudData[i + rgbOffset] / (float)byte.MaxValue,
                                (float)pointcloudData[i + rgbOffset + 1] / (float)byte.MaxValue,
                                (float)pointcloudData[i + rgbOffset + 2] / (float)byte.MaxValue,
                                1);

                                pointGeometry3D.Indices.Add(pointGeometry3D.Positions.Count);
                                pointGeometry3D.Positions.Add(pointPosition);
                                pointGeometry3D.Colors.Add(pointColor);
                            }
                        }
                    }
                }
            }

            return pointGeometry3D;
        }
    }
}
