#nullable enable
using System;

namespace ModernCSharpTechniques.Domain.Before
{
    public struct PointBefore
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        private double? distance;
        public double Distance
        {
            get
            {
                if(!distance.HasValue)
                    distance = Math.Sqrt(X * X + Y * Y);
                return distance.Value;
            }
        }

        public PointBefore(double x, double y)
        {
            X = x;
            Y = y;
            distance = default;
        }

        public static bool operator ==(PointBefore left, PointBefore right) =>
            left.X == right.X && left.Y == right.Y;

        public static bool operator !=(PointBefore left, PointBefore right) => !(left == right);

        public void SwapCoorsd()
        {
            var temp = X;
            X = Y;
            Y = temp;
        }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                PointBefore p = (PointBefore)obj;
                return (X == p.X) && (Y == p.Y);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
