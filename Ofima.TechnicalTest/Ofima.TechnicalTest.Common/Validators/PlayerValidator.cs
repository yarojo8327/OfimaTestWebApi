using FluentValidation;

using Ofima.TechnicalTest.WebApi.Models;

namespace Ofima.TechnicalTest.Common.Validators
{
    public class PlayerValidator : AbstractValidator<RegisterPlayer>
    {
        public PlayerValidator()
        {
            RuleFor(x => x.PlayerOne).NotNull().NotEmpty().Length(3, 30).OverridePropertyName("Jugador 1"); 
            RuleFor(x => x.PlayerTwo).NotNull().NotEmpty().Length(3, 30).OverridePropertyName("Jugador 2");
        }

    }
}
