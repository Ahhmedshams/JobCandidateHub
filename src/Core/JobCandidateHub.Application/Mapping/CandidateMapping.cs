using JobCandidateHub.Application.Dtos.Candidate;
using JobCandidateHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Application.Mapping;

public static class CandidateMapping
{
    public static Candidate ToCandidate(this CreateOrUpdateCandidateDto dto)
    {
        return new()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            CallTime = dto.CallTime,
            LinkedInUrl = dto.LinkedInUrl,
            GitHubUrl = dto.GitHubUrl,
            Comment = dto.Comment,
        };
    }
    public static Candidate Update(this Candidate candidate , CreateOrUpdateCandidateDto dto)
    {
        candidate.FirstName = dto.FirstName;
        candidate.LastName = dto.LastName;
        candidate.PhoneNumber = dto.PhoneNumber;
        candidate.Email = dto.Email;
        candidate.CallTime = dto.CallTime;
        candidate.LinkedInUrl = dto.LinkedInUrl;
        candidate.GitHubUrl = dto.GitHubUrl;
        candidate.Comment = dto.Comment;

        return candidate;
    }
}
