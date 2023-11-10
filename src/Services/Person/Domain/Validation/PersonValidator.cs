using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validation
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator() {

            RuleFor(person => person.FirstName).NotEmpty().WithMessage("Adı boş olamaz");
            RuleFor(person => person.LastName).NotEmpty().WithMessage("Soyadı boş olamaz");
        }
    }
}
