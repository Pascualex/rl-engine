namespace RLEngine.Utils
{
    [System.Serializable]
    public struct Size
    {
        public static Size Zero { get; } = new Size(0, 0);
        public static Size One { get; } = new Size(1, 1);

        public int X { get; set; }
        public int Y { get; set; }

        public Size(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Size(int xy)
        : this(xy, xy) { }

        public bool Contains(Coords coords)
        {
            return coords.X >= 0 && coords.X < X && coords.Y >= 0 && coords.Y < Y;
        }

        public static Size operator +(Size a, Size b)
        {
            return new Size(a.X + b.X, a.Y + b.Y);
        }

        public static Size operator -(Size a, Size b)
        {
            return new Size(a.X - b.X, a.Y - b.Y);
        }
    }
}