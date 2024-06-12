using JobCandidateHub.Application.Interfaces.Persistence;
using JobCandidateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Infrastructure.Persistence.Repositories;

internal class CandidateRepository : Repository<Candidate>, ICandidateRepository
{
    public CandidateRepository(JobCandidateHubDbContext context) : base(context)
    {
    }

}
