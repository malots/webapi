using FluentValidation;
using Malots.WebAPI.Domain.WorkModels;
using System;
using System.Diagnostics;

namespace Malots.WebAPI.Business.Validators
{
    [DebuggerDisplay("BaseValidator: {TWorkModel}")]
    public abstract class BaseValidator<TWorkModel> : AbstractValidator<TWorkModel> where TWorkModel : WorkModel
    {
        public BaseValidator()
        {
            RuleFor(m => m)
                .NotNull()
                .OnAnyFailure(x => {
                    throw new ArgumentNullException(nameof(x));
                });
        }
    }
}
