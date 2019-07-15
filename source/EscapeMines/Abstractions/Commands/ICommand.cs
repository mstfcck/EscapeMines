namespace EscapeMines.Abstractions.Commands
{
    /// <summary>
    ///     Declares an interface for executing an operation.
    /// </summary>
    public interface ICommand
    {
        void Execute();
    }
}