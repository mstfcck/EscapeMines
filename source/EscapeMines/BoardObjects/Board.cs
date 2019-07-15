using EscapeMines.Abstractions;

namespace EscapeMines.BoardObjects
{
    public class Board : IBoard
    {
        public Size Size { get; set; }

        public void SetSize(Size size)
        {
            Size = size;
        }

        public bool IsValid(ILocation location)
        {
            return location != null && location.X >= 0 && location.X <= Size.MaxX && location.Y >= 0 && location.Y <= Size.MaxY;
        }
    }
}