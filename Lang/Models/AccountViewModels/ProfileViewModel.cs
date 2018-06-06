using FluentValidation;
using Lang.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lang.Models.AccountViewModels
{
    public class ProfileViewModel
    {
        public ProfileViewModel() { }

        public ProfileViewModel(ExternalLoginInfo info)
        {
            if (info?.Principal != null)
            {
                this.Email = info.Principal.FindFirstValue(ClaimTypes.Email);
                this.Country = info.Principal.FindFirstValue(ClaimTypes.Country);

                this.Name = info.Principal.FindFirstValue(ClaimTypes.Name);
                if (!string.IsNullOrWhiteSpace(Name)
                    && Name.Contains(' ')
                    && !Name.EndsWith(' '))
                {
                    // Default to full first name plus last initial.
                    Name = Name.Split(' ')[0] + " " + Name.Split(' ')[1][0];
                }

                var dob = info.Principal.FindFirstValue(ClaimTypes.DateOfBirth);
                if (!string.IsNullOrWhiteSpace(dob)
                    && DateTime.TryParse(dob, out DateTime dateOfBirth)
                    && dateOfBirth.Year > 1900 && dateOfBirth.Year
                    <= DateTime.Now.Year)
                {
                    this.BirthYear = dateOfBirth.Year.ToString();
                }

                var gender = info.Principal.FindFirstValue(ClaimTypes.Gender);
                if (!string.IsNullOrWhiteSpace(gender) && gender.ToLower()[0] == 'm')
                {
                    this.Gender = Data.Gender.Male;
                }
                else if (!string.IsNullOrWhiteSpace(gender) && gender.ToLower()[0] == 'f')
                {
                    this.Gender = Data.Gender.Female;
                }
            }
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public Gender? Gender { get; set; }
        public string BirthYear { get; set; }
        public string Bio { get; set; }

        public int? GetBirthYear()
        {
            if (int.TryParse(this.BirthYear, out int birthYear)
                && birthYear >= 1900 
                && birthYear <= DateTime.Now.Year)
            {
                return birthYear;
            }

            return null;
        }
    }

    public class ProfileViewModelValidator : AbstractValidator<ProfileViewModel>
    {
        public ProfileViewModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Please enter your email address.")
                .EmailAddress().WithErrorCode("Email address is invalid.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Please enter your name.");
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Please enter your country.");
            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Please specify a gender.");
            RuleFor(x => x.BirthYear)
                .NotEmpty().WithMessage("Please enter your birth year.")
                .Must((model, x) => model.GetBirthYear() != null).WithMessage("Birth year is invalid.");
        }
    }
}
