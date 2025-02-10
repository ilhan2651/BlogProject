using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.BaseRepository.Concrete;

public class BlogProjectContextFactory : IDesignTimeDbContextFactory<BlogProjectContext>
{
    public BlogProjectContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BlogProjectContext>();
        var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
        if (string.IsNullOrEmpty(connectionString))
        {
            connectionString = "Host=localhost;Port=5432;Database=BlogProject;Username=postgres;Password=2651";
        }
        optionsBuilder
            .UseNpgsql(connectionString)
            .LogTo(Console.WriteLine);

        return new BlogProjectContext(optionsBuilder.Options);
    }
}