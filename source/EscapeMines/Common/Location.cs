using EscapeMines.Abstractions;

namespace EscapeMines.Common
{
    public class Location : ILocation
    {
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
