using XyloType.Application.Interfaces;

namespace XyloType.Application.Services;
public class InputCharMapperService : IInputCharMapperService
{
    public char Map(char value)
    {
        if (value == '\n')
        {
            return '↵';
        }
        
        if (value == '\t')
        {
            return '⟶';
        }

        return value;
    }
}
