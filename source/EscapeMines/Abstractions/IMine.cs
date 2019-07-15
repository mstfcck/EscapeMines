using EscapeMines.Common;

namespace EscapeMines.Abstractions
{
    public interface IMine
    {
        Location Location { get; set; }
        void Set(IBoard board, Location location);
    }
}