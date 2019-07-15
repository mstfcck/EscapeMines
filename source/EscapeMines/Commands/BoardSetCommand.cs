using EscapeMines.Abstractions;
using EscapeMines.Abstractions.Commands;
using EscapeMines.BoardObjects;

namespace EscapeMines.Commands
{
    public class BoardSetCommand : IBoardSetCommand
    {
        private IBoard _board;

        public BoardSetCommand(Size size)
        {
            Size = size;
        }

        public Size Size { get; }

        public void Set(IBoard board)
        {
            _board = board;
        }

        public void Execute()
        {
            _board.SetSize(Size);
        }
    }
}