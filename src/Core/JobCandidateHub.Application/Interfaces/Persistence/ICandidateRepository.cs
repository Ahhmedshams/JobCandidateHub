using JobCandidateHub.Domain.Entities;


namespace JobCandidateHub.Application.Interfaces.Persistence;

public interface ICandidateRepository : IRepository<Candidate>
{
    void Update(Candidate candidate);
}
