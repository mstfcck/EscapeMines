namespace EscapeMines.BoardObjects
{
    public class Size
    {
        public Size(int maxX, int maxY)
        {
            MaxX = maxX;
            MaxY = maxY;
        }

        public int MaxX { get; set; }
        public int MaxY { get; set; }
    }
}