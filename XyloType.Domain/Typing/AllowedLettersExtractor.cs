namespace XyloType.Domain.Typing;

public static class AllowedLettersExtractor
{
    public static string ExtractAllowedLetters(string allowedLetterFromUser, string textToLookInto)
    {
        // Detect all char in textToLookInto
        HashSet<char> detected =
        [
            .. textToLookInto.Where(c => !char.IsWhiteSpace(c))
        ];

        return new string(
        [
            .. allowedLetterFromUser.Where(detected.Contains),
            .. detected.Where(c => !allowedLetterFromUser.Contains(c))
        ]);
    }
}
