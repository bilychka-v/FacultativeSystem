using FacultativeSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FacultativeSystem.Infrastructure.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<CourseEntity>
{
    public void Configure(EntityTypeBuilder<CourseEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x=>x.StartDate).IsRequired();
        builder.Property(x=>x.EndDate).IsRequired();
        builder.Property(x => x.TeacherId).IsRequired();
        
    }
}