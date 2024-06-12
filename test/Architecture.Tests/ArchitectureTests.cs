

using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;

namespace Architecture.Tests;

public class ArchitectureTests
{
    private const string DomainNamespace = "JobCandidateHub.Domain";
    private const string ApplicationNamespace = "JobCandidateHub.Application";
    private const string InfrastructureNamespace = "JobCandidateHub.Infrastructure";
    private const string APINamespace = "JobCandidateHub.API";

 
    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange 
        var assembly = typeof(JobCandidateHub.Domain.AssemblyReference).Assembly;
        string[] otherProjects = [
            ApplicationNamespace,
            InfrastructureNamespace,
            APINamespace
            ];

        // Act 
        var testResult = Types.InAssembly(assembly)
              .ShouldNot()
              .HaveDependencyOnAll(otherProjects)
              .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange 
        var assembly = typeof(JobCandidateHub.Application.AssemblyReference).Assembly;
        string[] otherProjects = [
            InfrastructureNamespace,
            APINamespace
            ];

        // Act 
        var testResult = Types.InAssembly(assembly)
              .ShouldNot()
              .HaveDependencyOnAll(otherProjects)
              .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange 
        var assembly = typeof(JobCandidateHub.Infrastructure.AssemblyReference).Assembly;
        string[] otherProjects = [
            APINamespace
            ];
        // Act
        var testResult = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOn(APINamespace)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

  
   

}
