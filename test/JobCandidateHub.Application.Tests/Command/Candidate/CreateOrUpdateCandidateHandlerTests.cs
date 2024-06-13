using JobCandidateHub.Application.Command;
using JobCandidateHub.Application.Dtos.Candidate;
using JobCandidateHub.Application.Interfaces.Persistence;
using Moq;
using System.Linq.Expressions;
using JobCandidateHub.Domain.Entities;
using JobCandidateHub.Application.Interfaces.Cashing;
using FluentAssertions;
namespace JobCandidateHub.Application.Tests.Command;
public class CreateOrUpdateCandidateHandlerTests
{
    private readonly Mock<ICandidateRepository> _candidateRepositoryMock;
    private readonly Mock<ICachingService> _cachingServiceMock;
    private readonly CreateOrUpdateCandidateHandler _handler;

    public CreateOrUpdateCandidateHandlerTests()
    {
        _candidateRepositoryMock = new Mock<ICandidateRepository>();
        _cachingServiceMock = new Mock<ICachingService>();
        _handler = new CreateOrUpdateCandidateHandler(_candidateRepositoryMock.Object, _cachingServiceMock.Object);
    }

 
    [Fact]
    public async Task Handle_WhenCandidateExists_ShouldUpdateCandidate()
    {
        // Arrange
        var candidateDto = new CreateOrUpdateCandidateDto { Email = "test@example.com" };
        var command = new CreateOrUpdateCandidateCommand(candidateDto);
        var existingCandidate = new Candidate { Email = "test@example.com" };

        _cachingServiceMock
            .Setup(mock => mock.GetCachedData<Candidate>(It.IsAny<string>()))
            .Returns((Candidate)null);

        _candidateRepositoryMock
            .Setup(repo => repo.FindSingleAsync(It.IsAny<Expression<Func<Candidate, bool>>>()))
            .ReturnsAsync(existingCandidate);

        _candidateRepositoryMock
            .Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(candidateDto.Email);

        _candidateRepositoryMock.Verify(repo => repo.FindSingleAsync(It.IsAny<Expression<Func<Candidate, bool>>>()), Times.Once);
        _candidateRepositoryMock.Verify(repo => repo.Add(It.IsAny<Candidate>()), Times.Never);
        _candidateRepositoryMock.Verify(repo => repo.Update(existingCandidate), Times.Once);
        _candidateRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        _cachingServiceMock.Verify(mock => mock.SetCachedData(
            It.IsAny<string>(),
            It.IsAny<Candidate>(),
            It.IsAny<TimeSpan>()
        ), Times.Once);
    }
    [Fact]
    public async Task Handle_WhenCandidateExistsInCache_ShouldReturnCachedCandidate()
    {
        // Arrange
        var candidateDto = new CreateOrUpdateCandidateDto { Email = "test@example.com" };
        var command = new CreateOrUpdateCandidateCommand(candidateDto);
        var existingCandidate = new Candidate { Email = "test@example.com" };

        _cachingServiceMock
            .Setup(mock => mock.GetCachedData<Candidate>(It.IsAny<string>()))
            .Returns(existingCandidate);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(candidateDto.Email);

        _candidateRepositoryMock.Verify(repo => repo.FindSingleAsync(It.IsAny<Expression<Func<Candidate, bool>>>()), Times.Never);
        _candidateRepositoryMock.Verify(repo => repo.Add(It.IsAny<Candidate>()), Times.Never);
        _candidateRepositoryMock.Verify(repo => repo.Update(It.IsAny<Candidate>()), Times.Once);
        _candidateRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        _cachingServiceMock.Verify(mock => mock.SetCachedData(
            It.IsAny<string>(),
            It.IsAny<Candidate>(),
            It.IsAny<TimeSpan>()
        ), Times.Once);
    }


    [Fact]
    public async Task Handle_WhenCandidateDoesNotExist_ShouldAddCandidateAndCache()
    {
        // Arrange
        var candidateDto = new CreateOrUpdateCandidateDto { Email = "new@example.com" };
        var command = new CreateOrUpdateCandidateCommand(candidateDto);

        _cachingServiceMock
            .Setup(mock => mock.GetCachedData<Candidate>(It.IsAny<string>()))
            .Returns((Candidate)null);

        _candidateRepositoryMock
            .Setup(repo => repo.FindSingleAsync(It.IsAny<Expression<Func<Candidate, bool>>>()))
            .ReturnsAsync((Candidate)null);

        _candidateRepositoryMock
            .Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(candidateDto);
        _candidateRepositoryMock.Verify(repo => repo.Add(It.IsAny<Candidate>()), Times.Once);
        _candidateRepositoryMock.Verify(repo => repo.Update(It.IsAny<Candidate>()), Times.Never);
        _candidateRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        _cachingServiceMock.Verify(mock => mock.SetCachedData(
            It.IsAny<string>(),
            It.IsAny<Candidate>(),
            It.IsAny<TimeSpan>()
        ), Times.Once);
    }
}
