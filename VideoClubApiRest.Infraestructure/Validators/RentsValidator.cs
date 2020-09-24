using FluentValidation;
using Nest;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using VideoClubApiRest.Core.Entities;
using VisioForge.Shared.MediaFoundation.OPM;

namespace VideoClubApiRest.Infraestructure.Validators
{
    public class RentsValidator: AbstractValidator<Rents>
    {
        
        public RentsValidator() {
            RuleFor(rents => rents.ClientId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage(Messages("Null"))
                .NotEmpty()
                .Must(ValidGuid).WithMessage(Messages(""));
            RuleFor(rents => rents.ObjectId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage(Messages("Null"))
                .NotEmpty()
                .Must(ValidGuid).WithMessage(Messages(""));
            RuleFor(rents => rents.Detailsuntil)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage(Messages("Null"))
                .NotEmpty()
                .Must(ValidateDate).WithMessage(Messages(""));
            RuleFor(rents => rents.Detailssatus)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage(Messages("Null"))
                .NotEmpty()
                .Must(ValidStatus).WithMessage(Messages(""));
        }

        public bool ValidateDate(string date) {

            Regex regex = new Regex(@"([12]\d{3}\/(0[1-9]|1[0-2])\/(0[1-9]|[12]\d|3[01]))");
            bool isValid = regex.IsMatch(date.Trim());
            return isValid;
        }

        public bool ValidGuid(string guid) {
           return Guid.TryParse(guid, out Guid result);
        }

        public bool ValidStatus(string status) {
            return (status.Equals("RENT") || status.Equals("DELIVERY_TO_RENT") || status.Equals("RETURN") || status.Equals("DELIVERY_TO_RETURN"));
        }

        public string Messages(string type) {
            return (type.Equals("Null"))? "No puede ser nullo. " : "La entrada es inválida. ";
        }
    }
}
