namespace EscapeMines.Abstractions.Commands
{
    public interface IMineSetCommand : ICommand
    {
        void Set(IBoard board, IMine mine);
    }
}