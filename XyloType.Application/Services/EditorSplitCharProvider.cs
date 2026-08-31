using XyloType.Application.Interfaces;

namespace XyloType.Application.Services;

public class EditorSplitCharProvider : IEditorSplitCharProvider
{
    public char GetSplitCharacter()
    {
        // The Editor componant add \r on enter
        // So we split the text on \r to get the lines
        return '\r';
    }
}
