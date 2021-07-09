using System;

namespace RLEngine.Utils
{
    public struct Coords
    {
        public static Coords Zero { get; } = new Coords(0, 0);
        public static Coords One { get; } = new Coords(1, 1);
        public static Coords MinusOne { get; } = new Coords(-1, -1);
        public static Coords Up { get; } = new Coords(0, 1);
        public static Coords Right { get; } = new Coords(1, 0);
        public static Coords Down { get; } = new Coords(0, -1);
        public static Coords Left { get; } = new Coords(-1, 0);
        public static Coords UpRight { get; } = new Coords(1, 1);
        public static Coords DownRight { get; } = new Coords(1, -1);
        public static Coords DownLeft { get; } = new Coords(-1, -1);
        public static Coords UpLeft { get; } = new Coords(-1, 1);

        public int X { get; set; }
        public int Y { get; set; }

        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int GetDistance()
        {
            return GetDistance(Zero);
        }

        public int GetDistance(Coords coords)
        {
            return Math.Max(Math.Abs(X - coords.X), Math.Abs(Y - coords.Y));
        }

        public override bool Equals(object? o)
        {
            if (o is Coords coords) return (coords.X == X) && (coords.Y == Y);
            else return false;
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public static Coords operator +(Coords a, Coords b)
        {
            return new Coords(a.X + b.X, a.Y + b.Y);
        }

        public static Coords operator -(Coords a, Coords b)
        {
            return new Coords(a.X - b.X, a.Y - b.Y);
        }

        public static bool operator ==(Coords a, Coords b)
        {
            return Equals(a, b);
        }

        public static bool operator !=(Coords a, Coords b)
        {
            return !Equals(a, b);
        }

        public static Coords[] RandomizedDirections()
        {
            var directions = new Coords[] {
                Up,
                Right,
                Down,
                Left,
                UpRight,
                DownRight,
                DownLeft,
                UpLeft
            };

            new Random().Shuffle(directions);

            return directions;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }
    }
}