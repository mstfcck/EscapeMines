using EscapeMines.Abstractions;
using EscapeMines.Abstractions.Commands;
using EscapeMines.Common;

namespace EscapeMines.Commands
{
    public class ExitSetCommand : IExitSetCommand
    {
        private readonly Location _location;
        private IBoard _board;
        private IExit _exit;

        public ExitSetCommand(Location location)
        {
            _location = location;
        }

        public void Set(IBoard board, IExit exit)
        {
            _board = board;
            _exit = exit;
        }

        public void Execute()
        {
            _exit.Set(_board, _location);
        }
    }
}