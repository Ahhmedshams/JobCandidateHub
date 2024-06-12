using JobCandidateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobCandidateHub.Infrastructure.Persistence.Configurations;

public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.ToTable("candidate");
        builder.HasKey(p => p.Id);


        builder.Property(p => p.Id).HasColumnName("candidate_id");
        builder.Property(c => c.Email)
            .HasColumnName("email")
            .IsRequired();
        builder.HasIndex(c => c.Email).IsUnique();

        builder.Property(c => c.FirstName)
            .HasColumnName("first_name")
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.LastName)
            .HasColumnName("last_name")
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(c => c.PhoneNumber)
            .HasColumnName("phone_number")
            .IsRequired(false)
            .HasMaxLength(20);
        builder.Property(c => c.CallTime)
            .HasColumnName("call_time")
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(c => c.LinkedInUrl)
            .HasColumnName("linkedIn_url")
            .IsRequired(false)
            .HasMaxLength(200);
        builder.Property(c => c.GitHubUrl)
            .HasColumnName("gitHub_url")
            .IsRequired(false)
            .HasMaxLength(200);
        builder.Property(c => c.Comment)
            .HasColumnName("comment")
            .HasMaxLength(500);
    }
}
