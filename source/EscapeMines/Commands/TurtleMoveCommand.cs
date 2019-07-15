using EscapeMines.Abstractions;
using EscapeMines.Abstractions.Commands;
using EscapeMines.TurtleObjects;
using System.Collections.Generic;

namespace EscapeMines.Commands
{
    public class TurtleMoveCommand : ITurtleMoveCommand
    {
        private ITurtle _turtle;
        private IEnumerable<IMine> _mines;
        private IExit _exit;

        public TurtleMoveCommand(IList<Move> movements)
        {
            Movements = movements;
        }

        public IList<Move> Movements { get; set; }

        public void Set(ITurtle turtle, IEnumerable<IMine> mines, IExit exit)
        {
            _turtle = turtle;
            _mines = mines;
            _exit = exit;
        }

        public void Execute()
        {
            _turtle.Move(Movements, _mines, _exit);
        }
    }
}