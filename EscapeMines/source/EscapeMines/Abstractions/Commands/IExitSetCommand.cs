namespace EscapeMines.Abstractions.Commands
{
    public interface IExitSetCommand : ICommand
    {
        void Set(IBoard board, IExit exit);
    }
}