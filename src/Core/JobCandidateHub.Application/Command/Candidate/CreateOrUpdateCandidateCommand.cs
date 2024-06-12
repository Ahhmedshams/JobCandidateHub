using JobCandidateHub.Application.Dtos.Candidate;
using JobCandidateHub.Application.Interfaces.Persistence;
using JobCandidateHub.Application.Mapping;

namespace JobCandidateHub.Application.Command.Candidate;

public record CreateOrUpdateCandidateCommand (CreateOrUpdateCandidateDto Candidate) : IRequest<CreateOrUpdateCandidateDto>;

public class CreateOrUpdateCandidateHandler : IRequestHandler<CreateOrUpdateCandidateCommand, CreateOrUpdateCandidateDto>
{
    private readonly ICandidateRepository _candidateRepository;

    public CreateOrUpdateCandidateHandler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<CreateOrUpdateCandidateDto> Handle(CreateOrUpdateCandidateCommand request, CancellationToken cancellationToken)
    {
        var candidate = request.Candidate;

        var existingCandidate =await _candidateRepository.FindSingleAsync(c => c.Email == candidate.Email);

        if (existingCandidate != null)
        {
            existingCandidate = existingCandidate.Update(candidate);
            _candidateRepository.Update(existingCandidate);
        }
        else
        {
            _candidateRepository.Add(candidate.ToCandidate());
        }

        await _candidateRepository.SaveChangesAsync();


        return candidate;
    }
}
