using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Persistence.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<Training> Trainings {get; set;}
    public DbSet<Participant> Participants {get; set;}
    public DbSet<TrainingCategory> TrainingCategories {get; set;}
}
