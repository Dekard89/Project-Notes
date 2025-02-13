using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Note.DAL.EntityConfiguration;
using Note.Domain;

namespace Note.DAL;

public class NoteContext(IConfiguration configuration) : DbContext
{
    private readonly IConfiguration _configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Postgres"))
            .EnableSensitiveDataLogging();
        Database.EnsureCreated();
    }

    public DbSet<NoteEntity> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NoteConfiguration());
    }
}