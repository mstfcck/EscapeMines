using EscapeMines.Abstractions;

namespace EscapeMines.TurtleObjects
{
    /// <summary>
    ///     Turtle's x and y co-ordinates and cardinal compass point.
    /// </summary>
    public class TurtleLocation : ILocation
    {
        public int X { get; set; }
        public int Y { get; set; }

        public TurtleLocation(int x, int y, CardinalPoint cardinalPoint)
        {
            X = x;
            Y = y;
            CardinalPoint = cardinalPoint;
        }

        public CardinalPoint CardinalPoint { get; set; }
    }
}