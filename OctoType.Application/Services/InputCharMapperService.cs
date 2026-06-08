using OctoType.Application.Interfaces;

namespace OctoType.Application.Services;
public class InputCharMapperService : IInputCharMapperService
{
    public char Map(char value)
    {
        if (value == '\n')
        {
            return '↵';
        }

        return value;
    }
}
