using EscapeMines.TurtleObjects;
using System.Collections.Generic;

namespace EscapeMines.Abstractions.Commands
{
    public interface ITurtleMoveCommand : ICommand
    {
        IList<Move> Movements { get; set; }
        void Set(ITurtle turtle, IEnumerable<IMine> mines, IExit exit);
    }
}