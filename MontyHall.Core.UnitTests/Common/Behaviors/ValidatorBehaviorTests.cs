using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MontyHall.Core.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
namespace MontyHall.Core.UnitTests.Common.Behaviors
{
    public class ValidatorBehaviorTests
    {
        private readonly ValidatorBehavior<StubCommand, StubResponse> _sut;
        private readonly IValidator<StubCommand> _firstValidator;
        private readonly IValidator<StubCommand> _secondValidator;
        private readonly RequestHandlerDelegate<StubResponse> _requestHandlerDelegate;
        private readonly CancellationToken _cancellationToken;

        public ValidatorBehaviorTests()
        {
            _firstValidator = Substitute.For<IValidator<StubCommand>>();
            _secondValidator = Substitute.For<IValidator<StubCommand>>();
            var validatorList = new List<IValidator<StubCommand>> { _firstValidator, _secondValidator };

            _requestHandlerDelegate = Substitute.For<RequestHandlerDelegate<StubResponse>>();
            _cancellationToken = CancellationToken.None;
            _sut = new ValidatorBehavior<StubCommand, StubResponse>(validatorList, Substitute.For<ILogger<ValidatorBehavior<StubCommand, StubResponse>>>());
        }

        [Fact]
        public async Task Handle_CallsNextAfterSuccessfulValidation()
        {
            // Arrange
            var command = new StubCommand();
            var response = new StubResponse();
            _requestHandlerDelegate.Invoke().Returns(response);
            var validationResult = new ValidationResult();
            _firstValidator.Validate(command).Returns(validationResult);
            _secondValidator.Validate(command).Returns(validationResult);

            // Act
            var result = await _sut.Handle(command, _cancellationToken, _requestHandlerDelegate);

            // Assert
            result.Should().Be(response);
            await _requestHandlerDelegate.Received(1).Invoke();
        }

        [Fact]
        public async Task Handle_Throws_ArgumentException_WhenOneValidatorFails()
        {
            // Arrange
            var command = new StubCommand();
            var validationResultSuccess = new ValidationResult();
            const string validationError = "The value should not be null.";
            var validationResultFail = new ValidationResult(new[] { new ValidationFailure("First", validationError) });
            _firstValidator.Validate(command).Returns(validationResultSuccess);
            _secondValidator.Validate(command).Returns(validationResultFail);

            // Act
            Func<Task> act = async () => await _sut.Handle(command, _cancellationToken, _requestHandlerDelegate);

            // Assert
            act.Should().ThrowExactlyAsync<ArgumentException>()
                .WithMessage($"Command Validation Errors for type {typeof(StubCommand).Name}")
                .Result.WithInnerExceptionExactly<ValidationException>().WithMessage("Validation exception")
                .Which.Errors.Should().OnlyContain(failure => failure.ErrorMessage == validationError);
            await _requestHandlerDelegate.DidNotReceive().Invoke();
        }

        [Fact]
        public async Task Handle_Throws_ArgumentException_WhenBothValidatorFail()
        {
            // Arrange
            var command = new StubCommand();
            const string validationError = "The value should not be null.";
            var validationResultFail = new ValidationResult(new[] { new ValidationFailure("First", validationError) });
            _firstValidator.Validate(command).Returns(validationResultFail);
            _secondValidator.Validate(command).Returns(validationResultFail);

            // Act
            Func<Task> act = async () => await _sut.Handle(command, _cancellationToken, _requestHandlerDelegate);

            // Assert
            act.Should().ThrowExactlyAsync<ArgumentException>()
                .WithMessage($"Command Validation Errors for type {typeof(StubCommand).Name}")
                .Result.WithInnerExceptionExactly<ValidationException>().WithMessage("Validation exception")
                .Which.Errors.Should().OnlyContain(failure => failure.ErrorMessage == validationError);
            await _requestHandlerDelegate.DidNotReceive().Invoke();
        }

        public class StubCommand
        {
        }

        public class StubResponse
        {
        }
    }
}
