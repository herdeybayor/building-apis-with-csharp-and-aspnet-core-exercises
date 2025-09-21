using System.ComponentModel.DataAnnotations;
using FluentValidation;
using TheEmployeeAPI.Abstractions;

public class CreateEmployeeRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? SocialSecurityNumber { get; set; }

    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}

public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
{
    private readonly IRepository<Employee> _repository;

    public CreateEmployeeRequestValidator(IRepository<Employee> repository)
    {
        this._repository = repository;
        
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.SocialSecurityNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("SSN cannot be empty.")
            .MustAsync(BeUnique).WithMessage("SSN must be unique.");
    }

    private async Task<bool> BeUnique(string? ssn, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(ssn))
            return true;
        
        var existingEmployee = await _repository.GetEmployeeBySsn(ssn);
        return existingEmployee == null;
    }
}
