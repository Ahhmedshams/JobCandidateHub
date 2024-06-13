using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Application.Attribute;

public class SpecificUrlAttribute : ValidationAttribute
{
    private readonly string _allowedDomain;
    public SpecificUrlAttribute(string allowedDomain)
    {
        _allowedDomain = allowedDomain.ToLowerInvariant(); 
        ErrorMessage = $"The {{0}} field must be a valid {_allowedDomain} URL.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return ValidationResult.Success; 
        }

        var url = value.ToString();

        if (Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult))
        {
            var domain = uriResult.Host.ToLowerInvariant();
            if (domain.Contains(_allowedDomain))
            {
                return ValidationResult.Success;
            }
        }

        return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
    }
}