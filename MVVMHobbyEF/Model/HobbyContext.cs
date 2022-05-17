using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MVVMHobby.ViewModel;

namespace MVVMHobby.Model;

public class HobbyContext : DbContext
{
    public DbSet<HobbyVM> Hobbies { get; set; }

    public static IConfigurationRoot configuration;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();
            var connectionString = configuration.GetConnectionString("hobbyef");
            if (connectionString != null)
            {
                optionsBuilder.UseSqlServer(
                    connectionString
                    , options => options.MaxBatchSize(150));
            }
        }
    }
}