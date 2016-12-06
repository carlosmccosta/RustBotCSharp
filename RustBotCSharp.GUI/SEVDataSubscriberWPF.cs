using System;
using System.Diagnostics;
using Google.Protobuf;
using RustBotCSharp.Communication;
using RustBotCSharp.MessageConverter;
using Geometry = RustBotCSharp.Math.Geometry;

namespace RustBotCSharp.GUI
{
    public class SEVDataSubscriberWPF : SEVDataSubscriber
    {
        public SEVDataModel SEVDataModel { get; set; } = new SEVDataModel();

        public override CodedInputStream ReceiveData()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            CodedInputStream data = base.ReceiveData();
            stopWatch.Stop();
            SEVDataModel.DiagnosticsModel.MessageNetworkReceiveTimeMilliseconds = (double)stopWatch.Elapsed.Ticks / (double)TimeSpan.TicksPerMillisecond;
            SEVDataModel.DiagnosticsModel.MessageSizeInBytes = LastMessageSizeInBytes;
            return data;
        }

        public override SEVData ParseData(CodedInputStream serializedData)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            SEVData data = base.ParseData(serializedData);
            stopWatch.Stop();
            SEVDataModel.DiagnosticsModel.MessageParsingTimeMilliseconds = (double)stopWatch.Elapsed.Ticks / (double)TimeSpan.TicksPerMillisecond;
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
                        SEVDataModel.GNSSModel.Longitude = data.NavSatFix.Longitude;
                        SEVDataModel.GNSSModel.Altitude = data.NavSatFix.Altitude;

                        if (data.NavSatFix.Status != null)
                        {
                            switch (data.NavSatFix.Status.Status)
                            {
                                case -1:
                                {
                                    SEVDataModel.GNSSModel.Status = "NO_FIX";
                                    break;
                                }
                                case 0:
                                {
                                    SEVDataModel.GNSSModel.Status = "FIX";
                                    break;
                                }
                                case 1:
                                {
                                    SEVDataModel.GNSSModel.Status = "SBAS_FIX";
                                    break;
                                }
                                case 2:
                                {
                                    SEVDataModel.GNSSModel.Status = "GBAS_FIX";
                                    break;
                                }
                                default:
                                {
                                    SEVDataModel.GNSSModel.Status = "Other";
                                    break;
                                }
                            }

                            switch (data.NavSatFix.Status.Service)
                            {
                                case 1:
                                {
                                    SEVDataModel.GNSSModel.Service = "GPS";
                                    break;
                                }
                                case 2:
                                {
                                    SEVDataModel.GNSSModel.Service = "GLONASS";
                                    break;
                                }
                                case 4:
                                {
                                    SEVDataModel.GNSSModel.Service = "COMPASS";
                                    break;
                                }
                                case 8:
                                {
                                    SEVDataModel.GNSSModel.Service = "GALILEO";
                                    break;
                                }
                                default:
                                {
                                    SEVDataModel.GNSSModel.Service = "Other";
                                    break;
                                }
                            }
                        }
                    }

                    if (data.Odometry?.Pose?.Pose?.Position != null && data.Odometry?.Pose?.Pose?.Orientation != null)
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
                SEVDataModel.DiagnosticsModel.MessageProcessingTimeMilliseconds = (double)stopWatch.Elapsed.Ticks / (double)TimeSpan.TicksPerMillisecond; ;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
