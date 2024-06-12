using JobCandidateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace JobCandidateHub.Infrastructure.Persistence;

public class JobCandidateHubDbContext : DbContext
{
    public JobCandidateHubDbContext(DbContextOptions<JobCandidateHubDbContext> options): base(options)
    {
    }
    public DbSet<Candidate> Candidates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
