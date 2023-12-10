using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Flowy.Core.Contexts;

/// <summary>
/// context factory
/// </summary>
public class FluxContextFactory : IDesignTimeDbContextFactory<FlowyContext> {

  /// <summary>
  /// costruttore del context per il funzionamento della migratione
  /// dalla libreria it.Flowy.Engine
  /// </summary>
  /// <param name="args"></param>
  /// <returns>il context</returns>
  public FlowyContext CreateDbContext(string[] args) {
    string? basePath = Path.GetFullPath(@"../Flowy.Api/appsettings.json");
    basePath = Path.GetDirectoryName(basePath);
    if (basePath == null) { throw new Exception("No config file"); }

    IConfiguration configuration = new ConfigurationBuilder()
      .SetBasePath(basePath)
      .AddJsonFile("appsettings.json")
      .Build();

    var optionsBuilder = new DbContextOptionsBuilder<FlowyContext>();
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
    string? connectionString = configuration.GetConnectionString("Flowy");
    optionsBuilder.UseMySql(connectionString, serverVersion)
      .LogTo(Console.WriteLine, LogLevel.Information)
      .EnableSensitiveDataLogging()
      .EnableDetailedErrors();

    return new FlowyContext(optionsBuilder.Options);
  }
}