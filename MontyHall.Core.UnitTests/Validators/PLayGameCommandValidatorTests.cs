using FluentValidation.TestHelper;
using MontyHall.Core.Commands;
using MontyHall.Core.Validators;
using Xunit;

namespace MontyHall.Core.UnitTests.Validators
{
    public class PLayGameCommandValidatorTests
    {
        private readonly PlayGameCommandValidator _sut;

        public PLayGameCommandValidatorTests()
        {
            _sut = new PlayGameCommandValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void PlayGameCommand_ShouldHaveErrorWhenTriesZeroOrNegativeValue(int value)
        {
            // Act & Assert
            _sut.ShouldHaveValidationErrorFor(request => request.Tries, new PlayGameCommand { Tries = value });
        }

        [Theory]
        [InlineData(1)]
        public void PlayGameCommand_ShouldNotHaveErrorWhenTriesIsPositiveValue(int value)
        {
            // Act & Assert
            _sut.ShouldNotHaveValidationErrorFor(request => request.Tries, new PlayGameCommand { Tries = value });
        }

    }
}
