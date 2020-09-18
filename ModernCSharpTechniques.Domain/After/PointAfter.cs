#nullable enable
using System;

namespace ModernCSharpTechniques.Domain.After
{
    public struct PointAfter
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        private double? distance;
        public double Distance
        {
            get
            {
                if (!distance.HasValue)
                    distance = Math.Sqrt(X * X + Y * Y);
                return distance.Value;
            }
        }

        public PointAfter(double x, double y) => (X, Y, distance) = (x, y, default);

        public static bool operator ==(PointAfter left, PointAfter right) =>
            (left.X, left.Y) == (right.X, right.Y);

        public static bool operator !=(PointAfter left, PointAfter right) => !(left == right);

        public void SwapCoords() => (X, Y) = (Y, X);

        //returns false when obj is null (is operator works in such way)
        public override bool Equals(object? obj) =>
            obj is PointAfter other && this == other;

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
