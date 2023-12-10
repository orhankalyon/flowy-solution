using Flowy.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Flowy.Core.Contexts;

/// <summary>
/// context per la connessione al database
/// </summary>
public class FlowyContext : DbContext {

  public DbSet<Tenant>? Tenants { get; set; }
  public DbSet<Scope>? Scopes { get; set; }
  public DbSet<Draft>? Drafts { get; set; }
  public DbSet<DraftTrack>? DraftTracks { get; set; }
  public DbSet<Process>? Processes { get; set; }
  public DbSet<Instance>? Instances { get; set; }
  public DbSet<InstanceTrack>? InstanceTracks { get; set; }
  public DbSet<InstanceData>? InstanceDatas { get; set; }
  public DbSet<Interaction>? Interactions { get; set; }
  public DbSet<InteractionTrack>? InteractionTracks { get; set; }

  /// <summary>
  /// costruttore del context
  /// </summary>
  /// <param name="options">option per la configurazione</param>
  public FlowyContext(DbContextOptions<FlowyContext> options) : base(options){
    ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
  }

  /// <summary>
  /// on configuring
  /// </summary>
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {      
    //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
  }

  /// <summary>
  /// in model creationg
  /// </summary>
  /// <param name="modelBuilder">model builder</param>
  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    //modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.IdUser, ur.IdRole });
    //modelBuilder.Entity<RolePermission>().HasKey(rp => new { rp.IdRole, rp.IdPermission });
    //modelBuilder.Entity<Deployment>().HasIndex(d => d.Identifier).IsUnique();
    //modelBuilder.Entity<Process>().HasIndex(p => p.Identifier).IsUnique();
    //modelBuilder.Entity<Instance>().HasIndex(i => i.Identifier).IsUnique();
  }
}