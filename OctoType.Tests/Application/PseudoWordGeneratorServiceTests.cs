using FluentAssertions;

using OctoType.Application.ValueObjects;

namespace OctoType.Tests.Application;

public class PseudoWordOptionsTests
{
    [Fact]
    public void OptionsAreDifferents()
    {
        // Arrange
        PseudoWordOptions opt = new("abcABC", 3,3);
        PseudoWordOptions opt2 = new("abcAB", 3, 3);

        // Act
        bool diff = opt != opt2;


        // Assert
        diff.Should().BeTrue();
    }

    [Fact]
    public void OptionsAreTheSame()
    {
        // Arrange
        PseudoWordOptions opt = new("abcABC", 3, 3);
        PseudoWordOptions opt2 = new("abcABC", 3, 3);

        // Act
        bool diff = opt != opt2;


        // Assert
        diff.Should().BeFalse();
    }

}
