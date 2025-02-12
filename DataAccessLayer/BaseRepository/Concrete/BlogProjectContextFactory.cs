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
            // Lokal çalıştırırken kullan
            connectionString = "Host=localhost;Port=5432;Database=BlogProject;Username=postgres;Password=2651";
        }

        // Render için bağlantı dizesini düzenle
        if (!string.IsNullOrEmpty(connectionString) && connectionString.StartsWith("postgres://"))
        {
            connectionString = ConvertDatabaseUrlToConnectionString(connectionString);
        }

        optionsBuilder
            .UseNpgsql(connectionString)
            .LogTo(Console.WriteLine);

        return new BlogProjectContext(optionsBuilder.Options);
    }

    // Render'ın verdiği DATABASE_URL bağlantısını uygun hale getiren metod
    private static string ConvertDatabaseUrlToConnectionString(string databaseUrl)
    {
        var uri = new Uri(databaseUrl);
        var host = uri.Host;
        var database = uri.AbsolutePath.TrimStart('/');
        var userInfo = uri.UserInfo.Split(':');
        var username = userInfo[0];
        var password = userInfo[1];

        return $"Host={host};Port=5432;Database={database};Username={username};Password={password};SslMode=Require;Trust Server Certificate=true";
    }
}
