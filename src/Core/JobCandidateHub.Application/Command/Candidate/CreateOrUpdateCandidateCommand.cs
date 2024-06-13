using JobCandidateHub.Application.Dtos.Candidate;
using JobCandidateHub.Application.Interfaces.Cashing;
using JobCandidateHub.Application.Interfaces.Persistence;
using JobCandidateHub.Application.Mapping;
using JobCandidateHub.Domain.Entities;

namespace JobCandidateHub.Application.Command;

public record CreateOrUpdateCandidateCommand (CreateOrUpdateCandidateDto Candidate) : IRequest<CreateOrUpdateCandidateDto>;

public class CreateOrUpdateCandidateHandler : IRequestHandler<CreateOrUpdateCandidateCommand, CreateOrUpdateCandidateDto>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ICachingService _caching;

    public CreateOrUpdateCandidateHandler(ICandidateRepository candidateRepository, ICachingService caching)
    {
        _candidateRepository = candidateRepository;
        _caching = caching;
    }

    public async Task<CreateOrUpdateCandidateDto> Handle(CreateOrUpdateCandidateCommand request, CancellationToken cancellationToken)
    {
        var candidate = request.Candidate;

        var existingCandidate = _caching.GetCachedData<Candidate>(GetCacheKeyForCandidate(candidate.Email));
        if (existingCandidate == null)
        {
            // Candidate not found in cache, fetch from repository
            existingCandidate = await _candidateRepository.FindSingleAsync(c => c.Email == candidate.Email);
        }

        if (existingCandidate != null)
        {
            existingCandidate = existingCandidate.Update(candidate);
            _candidateRepository.Update(existingCandidate);
        }
        else
        {
            var newCandidate = candidate.ToCandidate();
            _candidateRepository.Add(newCandidate);
            existingCandidate = newCandidate;  
        }

        await _candidateRepository.SaveChangesAsync();

        _caching.SetCachedData(GetCacheKeyForCandidate(candidate.Email), existingCandidate, TimeSpan.FromMinutes(10));

        return candidate;
    }
    private string GetCacheKeyForCandidate(string email)
    {
        return $"Candidate_{email.ToLower()}";
    }
}
