using JobCandidateHub.Application.Attribute;
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
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string? CallTime { get; set; }
    [MaxLength(200)]
    [SpecificUrl("linkedin.com")]
    public string? LinkedInUrl { get; set; }
    [MaxLength(200)]
    [SpecificUrl("github.com")]
    public string? GitHubUrl { get; set; }
    [Required]
    [MaxLength(500)]
    public string Comment { get; set; } = string.Empty;
}
