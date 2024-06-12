using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Application.Dtos.Candidate;

public class CreateOrUpdateCandidateDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string CallTime { get; set; }
    public string LinkedInUrl { get; set; }
    public string GitHubUrl { get; set; }
    [Required]
    public string Comment { get; set; } = string.Empty;
}
