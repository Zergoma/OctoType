using FluentAssertions;

using XyloType.Domain.Typing;

namespace XyloType.Tests.Domain.Typing;

public class AllowedLettersExtractorTest
{
    [Theory ]
    [InlineData("abcd","abcd", "abcd")]     // same
    [InlineData("abcd", "dcba", "abcd")]    // all - order
    [InlineData("abcd","ab", "ab")]         // subset
    [InlineData("ab", "cd", "cd")]          // juste text, no user allowed
    public void Allow_good(string allowedUser, string text, string expecded)
    {

        string extracted = AllowedLettersExtractor.ExtractAllowedLetters(allowedUser, text);

        extracted.Should().Be(expecded);

    }
}
