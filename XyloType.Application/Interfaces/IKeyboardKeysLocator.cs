using XyloType.Domain.Enums;
using XyloType.Domain.Models;

namespace XyloType.Application.Interfaces
{
    public interface IKeyboardKeysLocator
    {
        public KeyboardLayout GetKeyboardType {  get; }
        public IReadOnlyDictionary<char, KeyInfo> KeyLocator { get; }
    }
}