using FluentValidation;
using TheEmployeeAPI.Abstractions;
using TheEmployeeAPI.Employees;

public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
{
    private readonly IRepository<Employee> _repository;

    public CreateEmployeeRequestValidator(IRepository<Employee> repository)
    {
        this._repository = repository;

        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
        RuleFor(x => x.PhoneNumber).Matches(@"^\+?[1-9]\d{1,14}$").When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        RuleFor(x => x.SocialSecurityNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("SSN cannot be empty.")
            .MustAsync(BeUnique).WithMessage("SSN must be unique.");

        private async Task<bool> BeUnique(string ssn, CancellationToken token)
        {
        return await _repository.GetEmployeeBySsn(ssn) != null;
        }
    }
}