namespace RustBotCSharp.Math
{
    public class Geometry
    {
        public static bool QuaternionToEuler(double qx, double qy, double qz, double qw, out double heading, out double attitude, out double bank)
        {
            double sqw = qw * qw;
            double sqx = qx * qx;
            double sqy = qy * qy;
            double sqz = qz * qz;
            double unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
            double test = qx * qy + qz * qw;
            if (test > 0.499 * unit)
            { // singularity at north pole
                heading = 2 * System.Math.Atan2(qx, qw);
                attitude = System.Math.PI / 2;
                bank = 0;
                return false;
            }
            if (test < -0.499 * unit)
            { // singularity at south pole
                heading = -2 * System.Math.Atan2(qx, qw);
                attitude = -System.Math.PI / 2;
                bank = 0;
                return false;
            }
            heading = System.Math.Atan2(2 * qy * qw - 2 * qx * qz, sqx - sqy - sqz + sqw);
            attitude = System.Math.Asin(2 * test / unit);
            bank = System.Math.Atan2(2 * qx * qw - 2 * qy * qz, -sqx + sqy - sqz + sqw);
            return true;
        }

        public static double DegreeToRadian(double angle)
        {
            return System.Math.PI * angle / 180.0;
        }

        public static double RadianToDegree(double angle)
        {
            return angle * (180.0 / System.Math.PI);
        }
    }
}
