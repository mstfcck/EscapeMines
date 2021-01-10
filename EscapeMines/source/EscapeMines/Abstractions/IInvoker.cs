using EscapeMines.Abstractions.Commands;
using System.Collections.Generic;

namespace EscapeMines.Abstractions
{
    /// <summary>
    ///     The 'Invoker' class
    /// </summary>
    public interface IInvoker
    {
        void SetCommands(IEnumerable<ICommand> commands);
        void InvokeCommands();
        string GetLatestTurtleLocation();
        string GetResult();
    }
}