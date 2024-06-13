using JobCandidateHub.Application.Command.Candidate;
using JobCandidateHub.Application.Dtos.Candidate;
using JobCandidateHub.Application.Interfaces.Persistence;
using Moq;
using System.Linq.Expressions;
using JobCandidateHub.Domain.Entities;
namespace JobCandidateHub.Application.Tests.Command;
public class CreateOrUpdateCandidateHandlerTests
{
    private readonly Mock<ICandidateRepository> _candidateRepositoryMock;
    private readonly CreateOrUpdateCandidateHandler _handler;

    public CreateOrUpdateCandidateHandlerTests()
    {
        _candidateRepositoryMock = new Mock<ICandidateRepository>();
        _handler = new CreateOrUpdateCandidateHandler(_candidateRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenCandidateExists_ShouldUpdateCandidate()
    {
        // Arrange
        var candidateDto = new CreateOrUpdateCandidateDto { Email = "test@example.com" };
        var command = new CreateOrUpdateCandidateCommand(candidateDto);
        var existingCandidate = new Candidate { Email = "test@example.com" };

        _candidateRepositoryMock
            .Setup(repo => repo.FindSingleAsync(It.Is<Expression<Func<Candidate, bool>>>(expr => expr.Compile().Invoke(existingCandidate))))
            .ReturnsAsync(existingCandidate);

        _candidateRepositoryMock.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(candidateDto, result);
        _candidateRepositoryMock.Verify(repo => repo.Update(existingCandidate), Times.Once);
        _candidateRepositoryMock.Verify(repo => repo.Add(It.IsAny<Candidate>()), Times.Never);
        _candidateRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenCandidateDoesNotExist_ShouldAddCandidate()
    {
        // Arrange
        var candidateDto = new CreateOrUpdateCandidateDto { Email = "new@example.com" };
        var command = new CreateOrUpdateCandidateCommand(candidateDto);

        _candidateRepositoryMock.Setup(repo => repo.FindSingleAsync(It.IsAny<Expression<Func<Candidate, bool>>>()))
            .ReturnsAsync((Candidate)null);

        _candidateRepositoryMock
            .Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(candidateDto, result);
        _candidateRepositoryMock.Verify(repo => repo.Add(It.IsAny<Candidate>()), Times.Once);
        _candidateRepositoryMock.Verify(repo => repo.Update(It.IsAny<Candidate>()), Times.Never);
        _candidateRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
