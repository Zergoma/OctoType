using OctoType.Domain.Enums;
using OctoType.Domain.Models;

namespace OctoType.Application.Interfaces
{
    public interface IKeyboardKeyLocator
    {
        public KeyboardLayout GetKeyboardType {  get; }
        public IReadOnlyDictionary<char, KeyInfo> KeyLocator { get; }
    }
}