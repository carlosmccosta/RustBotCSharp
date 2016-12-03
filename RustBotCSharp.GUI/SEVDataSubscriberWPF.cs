using System;
using System.Diagnostics;
using RustBotCSharp.Communication;
using RustBotCSharp.MessageConverter;
using Geometry = RustBotCSharp.Math.Geometry;

namespace RustBotCSharp.GUI
{
    public class SEVDataSubscriberWPF : SEVDataSubscriber
    {
        public SEVDataModel SEVDataModel { get; set; } = new SEVDataModel();

        public override SEVData ReceiveData()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            SEVData data = base.ReceiveData();
            stopWatch.Stop();
            SEVDataModel.DiagnosticsModel.MessageSizeInBytes = LastMessageSizeInBytes;
            SEVDataModel.DiagnosticsModel.MessageParsingTimeMilliseconds = stopWatch.ElapsedMilliseconds;
            return data;
        }

        public override bool ProcessData(SEVData data)
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                if (data != null)
                {
                    if (data.LeftImage != null)
                        SEVDataModel.LeftImageWriteableBitmap = ImageConverter.ConvertToWrittableBitmap(data.LeftImage);

                    if (data.RightImage != null)
                        SEVDataModel.RightImageWriteableBitmap = ImageConverter.ConvertToWrittableBitmap(data.RightImage);

                    if (data.PointCloud != null)
                        SEVDataModel.PointGeometry3D = PointCloudConverter.ConvertToPointGeometry3D(data.PointCloud);

                    if (data.NavSatFix != null)
                    {
                        SEVDataModel.GNSSModel.Latitude = data.NavSatFix.Latitude;
                        SEVDataModel.GNSSModel.Longitude= data.NavSatFix.Longitude;
                        SEVDataModel.GNSSModel.Altitude = data.NavSatFix.Altitude;
                    }

                    if (data.Odometry?.Pose != null)
                    {
                        SEVDataModel.StereoSystemPoseModel.X = data.Odometry.Pose.Pose.Position.X;
                        SEVDataModel.StereoSystemPoseModel.Y = data.Odometry.Pose.Pose.Position.Y;
                        SEVDataModel.StereoSystemPoseModel.Z = data.Odometry.Pose.Pose.Position.Z;

                        double heading, attitude, bank;
                        Geometry.QuaternionToEuler(
                            data.Odometry.Pose.Pose.Orientation.X,
                            data.Odometry.Pose.Pose.Orientation.Y,
                            data.Odometry.Pose.Pose.Orientation.Z,
                            data.Odometry.Pose.Pose.Orientation.W,
                            out heading, out attitude, out bank);

                        SEVDataModel.StereoSystemPoseModel.Heading = Geometry.RadianToDegree(heading);
                        SEVDataModel.StereoSystemPoseModel.Attitude = Geometry.RadianToDegree(attitude);
                        SEVDataModel.StereoSystemPoseModel.Bank = Geometry.RadianToDegree(bank);
                    }
                }
                stopWatch.Stop();
                SEVDataModel.DiagnosticsModel.MessageProcessingTimeMilliseconds = stopWatch.ElapsedMilliseconds;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
