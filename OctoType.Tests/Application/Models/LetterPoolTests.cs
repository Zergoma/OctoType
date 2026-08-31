using FluentAssertions;

using FluentValidation.Results;

using OctoType.Application.Models;
using OctoType.Application.Validators;
using OctoType.Application.ValueObjects;

namespace OctoType.Tests.Application.Models;

public class LetterPoolTests
{
    private readonly FilteredCharTypesValidator validator = new();

    [Fact]
    public void FilterBasicVowelsSuccess()
    {
        // Arrange
        string lettersToTest = "aeiouy";

        // Act
        FilteredCharTypes filterByType = LetterPool.FilterToType(lettersToTest);


        // Assert
        filterByType.Vowels.Should().NotBeEmpty();
        LetterPool.VowelsHash.Should().Contain(filterByType.Vowels);

        filterByType.Consonants.Should().BeEmpty();


        ValidationResult resuValidator = validator.Validate(filterByType);
        bool resu = resuValidator.IsValid;
        resu.Should().BeTrue();
    }

    [Fact]
    public void FilterConsonantSuccess()
    {
        // Arrange
        string lettersToTest = "bcdfghjklmnpqrstvwxz";

        // Act
        FilteredCharTypes filterByType = LetterPool.FilterToType(lettersToTest);

        // Assert
        filterByType.Consonants.Should().NotBeEmpty();
        LetterPool.ConsonantsHash.Should().Contain(filterByType.Consonants);

        filterByType.Vowels.Should().BeEmpty();

        ValidationResult resuValidator = validator.Validate(filterByType);
        bool resu = resuValidator.IsValid;
        resu.Should().BeTrue();
    }


    [Fact]
    public void FilterVowelsConsonantSuccess()
    {
        // Arrange
        string lettersToTest = "aeiouy" + "bcdfghjklmnpqrstvwxz";

        // Act
        FilteredCharTypes filterByType = LetterPool.FilterToType(lettersToTest);

        // Assert
        filterByType.Vowels.Should().NotBeEmpty();
        filterByType.Consonants.Should().NotBeEmpty();

        LetterPool.VowelsHash.Should().Contain(filterByType.Vowels);
        LetterPool.ConsonantsHash.Should().Contain(filterByType.Consonants);

        ValidationResult resuValidator = validator.Validate(filterByType);
        bool resu = resuValidator.IsValid;
        resu.Should().BeTrue();
    }


    [Fact]
    public void FilterNoLetters()
    {
        // Arrange
        string lettersToTest = "";

        // Act
        FilteredCharTypes filterByType = LetterPool.FilterToType(lettersToTest);

        // Assert
        filterByType.Vowels.Should().BeEmpty();
        filterByType.Consonants.Should().BeEmpty();

        ValidationResult resuValidator = validator.Validate(filterByType);
        bool resu = resuValidator.IsValid;
        resu.Should().BeFalse();
    }

}
