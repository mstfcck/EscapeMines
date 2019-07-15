using EscapeMines.TurtleObjects;
using System.Collections.Generic;

namespace EscapeMines.Abstractions
{
    public interface ITurtle
    {
        TurtleLocation Location { get; set; }
        string Result { get; set; }
        void Set(IBoard board, TurtleLocation location);
        void Move(IEnumerable<Move> movements, IEnumerable<IMine> mines, IExit exit);
    }
}