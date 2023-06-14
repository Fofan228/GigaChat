using System.Reflection;

using GigaChat.Core.Common.Entities.ChatMessages;
using GigaChat.Core.Common.Entities.ChatRooms;
using GigaChat.Core.Common.Entities.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GigaChat.Data;

public class GigaChatDbContext : DbContext
{
    public const string ConnectionStringName = "GigaChatDb";

    public static void Configure(
        DbContextOptionsBuilder optionsBuilder,
        IConfiguration configuration,
        Assembly? migrationsAssembly = null)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(ConnectionStringName), builder =>
        {
            if (migrationsAssembly != null) builder.MigrationsAssembly(migrationsAssembly.GetName().ToString());
        });
        optionsBuilder.EnableSensitiveDataLogging(false);
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    public GigaChatDbContext(DbContextOptions<GigaChatDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GigaChatDbContext).Assembly);
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<ChatMessage> ChatMessages { get; set; } = null!;
    public DbSet<ChatRoom> ChatRooms { get; set; } = null!;
}