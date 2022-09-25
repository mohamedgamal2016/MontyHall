using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using MontyHall.Core.Factories;
using MontyHall.Core.Services;
using MontyHall.Core.Commands;
using MontyHall.Core.Common.Commands;
using MontyHall.Core.Common.Response;
using MontyHall.Core.Models;
using Xunit;
namespace MontyHall.Core.UnitTests.Services
{
    public class GameEngineServiceTests
    {
        private const int MIN_WIN_SWITCH = 60;
        private const int MAX_LOSS_SWITCH = 40;

        private const int MAX_WIN_STAY = 40;
        private const int MIN_LOSS_STAY = 60;

        private readonly IDoorFactory _doorFactory;
        private readonly GameEngineService _sut;

        public GameEngineServiceTests()
        {
            _doorFactory = Substitute.For<IDoorFactory>();

            _sut = new GameEngineService(_doorFactory);
        }

        [Theory]
        [MemberData(nameof(CreatPlayGameCommand))]
        public async Task Play_Success(int tries, bool isSwitchEnabled)
        {
            var playGameCommand = new PlayGameCommand {
                Tries = tries,
                IsSwitchStrategyEnabled = isSwitchEnabled
            };

            var doors = CreateDoors();
            _doorFactory.Create().Returns(doors);
            

            // Act
            var result = await _sut.Play(playGameCommand);

            // Assert
            if (isSwitchEnabled)
            {
                result.Data.Payload.WinPercentage.Should().BeGreaterThan(MIN_WIN_SWITCH);
                result.Data.Payload.LossPercentage.Should().BeLessThan(MAX_LOSS_SWITCH);
            }
            else
            {
                result.Data.Payload.WinPercentage.Should().BeLessThan(MAX_WIN_STAY);
                result.Data.Payload.LossPercentage.Should().BeGreaterThan(MIN_LOSS_STAY);
            }
            result.Should().BeOfType<SuccessCommandResult<PayloadResponse<ScoreBoard>>>();
            result.Errors.Should().BeEmpty();
            result.ResultType.Should().Be(ResultType.Ok);
        }

        [Fact]
        public async Task Play_ReturnsUnexpectedCommandResult_WhenExceptionIsThrown()
        {
            // Arrange
            var playGameCommand = new PlayGameCommand
            {
            };

            _doorFactory.Create().Throws<Exception>();

            // Act
            var result = await _sut.Play(playGameCommand);

            // Assert
            result.Should().BeOfType<UnexpectedCommandResult<PayloadResponse<ScoreBoard>>>();
            result.Errors.Should().Contain("There was an unexpected problem.");
            result.ResultType.Should().Be(ResultType.Unexpected);
        }

        private static List<Door> CreateDoors()
        {
            return new List<Door>
            {
                new Door {
                    IsPrize = false,
                    Name = "Goat"
                },
                new Door
                {
                    IsPrize = false,
                    Name = "Goat"
                },
                new Door
                {
                    IsPrize = true,
                    Name = "Car"
                }
            };
        }

        public static IEnumerable<object[]> CreatPlayGameCommand()
        {
            yield return new object[] { 100, true };
            yield return new object[] { 100, false };
            yield return new object[] { 1000, true };
            yield return new object[] { 1000, false };
            yield return new object[] { 1000000, true };
            yield return new object[] { 1000000, false };
        }
    }
}
