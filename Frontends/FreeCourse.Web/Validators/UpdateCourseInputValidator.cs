﻿using FluentValidation;
using FreeCourse.Web.Models.Catalog;

namespace FreeCourse.Web.Validators
{
    public class UpdateCourseInputValidator:AbstractValidator<UpdateCourseInput>
    {
        public UpdateCourseInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("isim alanı boş olamaz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("açıklama alanı boş olamaz.");
            RuleFor(x => x.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("açıklama alanı boş olamaz.");
            RuleFor(x => x.Price).NotEmpty().WithMessage("fiyat alanı boş olamaz.").ScalePrecision(2, 6).WithMessage("hatalı fiyat formatı");        
        }
    }
}
