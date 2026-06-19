using FluentValidation;
using WarehouseApi.Application.Categories.Dtos;

namespace WarehouseApi.Application.Categories.Validation;

internal class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Название категории не должно быть пустым");
    }
}