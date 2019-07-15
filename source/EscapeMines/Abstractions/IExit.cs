using EscapeMines.Common;

namespace EscapeMines.Abstractions
{
    public interface IExit
    {
        Location Location { get; set; }
        void Set(IBoard board, Location location);
    }
}