namespace EscapeMines.Abstractions.Commands
{
    public interface ITurtleSetCommand : ICommand
    {
        void Set(IBoard board, ITurtle turtle);
    }
}