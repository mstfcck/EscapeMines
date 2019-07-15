using EscapeMines.Abstractions;
using EscapeMines.Abstractions.Commands;
using EscapeMines.Common;

namespace EscapeMines.Commands
{
    public class MineSetCommand : IMineSetCommand
    {
        private readonly Location _location;
        private IBoard _board;
        private IMine _mine;

        public MineSetCommand(Location location)
        {
            _location = location;
        }

        public void Set(IBoard board, IMine mine)
        {
            _board = board;
            _mine = mine;
        }

        public void Execute()
        {
            _mine.Set(_board, _location);
        }
    }
}