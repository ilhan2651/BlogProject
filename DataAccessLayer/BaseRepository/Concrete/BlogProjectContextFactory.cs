using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.BaseRepository.Concrete;

public class BlogProjectContextFactory : IDesignTimeDbContextFactory<BlogProjectContext>
{
    public BlogProjectContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BlogProjectContext>();
        optionsBuilder
            .UseNpgsql("Host=localhost;Port=5432;Database=BlogProject;Username=postgres;Password=2651")
               .LogTo(Console.WriteLine);

        return new BlogProjectContext(optionsBuilder.Options);
    }
}