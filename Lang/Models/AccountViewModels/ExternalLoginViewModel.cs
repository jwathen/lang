using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lang.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Country { get; set; }
        public Gender? Gender { get; set; }
        public string DateOfBirth { get; set; }
    }

    public class ExternalLoginViewModelValidator : AbstractValidator<ExternalLoginViewModel>
    {
        public ExternalLoginViewModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
