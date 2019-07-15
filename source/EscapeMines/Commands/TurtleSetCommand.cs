using EscapeMines.Abstractions;
using EscapeMines.Abstractions.Commands;
using EscapeMines.TurtleObjects;

namespace EscapeMines.Commands
{
    public class TurtleSetCommand : ITurtleSetCommand
    {
        private readonly TurtleLocation _location;
        private IBoard _board;
        private ITurtle _turtle;

        public TurtleSetCommand(TurtleLocation location)
        {
            _location = location;
        }

        public void Set(IBoard board, ITurtle turtle)
        {
            _board = board;
            _turtle = turtle;
        }

        public void Execute()
        {
            _turtle.Set(_board, _location);
        }
    }
}