using EscapeMines.BoardObjects;
using EscapeMines.Common;

namespace EscapeMines.Abstractions
{
    /// <summary>
    ///     The 'Receiver' class
    /// </summary>
    public interface IBoard
    {
        Size Size { get; set; }
        void SetSize(Size size);
        bool IsValid(ILocation location);
    }
}