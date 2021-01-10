using EscapeMines.Abstractions.Commands;
using System.Collections.Generic;

namespace EscapeMines.Abstractions.Services
{
    public interface ICommandParserService
    {
        /// <summary>
        ///     Parses all instructions and returns the command interface list.
        /// </summary>
        /// <param name="instructions"></param>
        IEnumerable<ICommand> Parse(string instructions);
    }
}