namespace EscapeMines.Abstractions.Commands
{
    public interface IBoardSetCommand : ICommand
    {
        void Set(IBoard board);
    }
}