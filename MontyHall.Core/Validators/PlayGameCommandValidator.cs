using FluentValidation;
using MontyHall.Core.Commands;

namespace MontyHall.Core.Validators
{
    public class PlayGameCommandValidator : AbstractValidator<PlayGameCommand>
    {
        public PlayGameCommandValidator()
        {
            RuleFor(command => command.Tries)
                .NotNull()
                .Must(tries => tries > 0)
                .WithMessage("Game Tries should be a positive number");

            RuleFor(command => command.IsSwitchStrategyEnabled)
                .NotNull()
                .WithMessage("Game strategy should be entered");
        }
    }
}
