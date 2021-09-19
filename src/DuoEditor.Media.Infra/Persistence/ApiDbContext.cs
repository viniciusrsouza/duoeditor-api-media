using DuoEditor.Media.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DuoEditor.Media.Infra.Persistence
{
  public class ApiDbContext : DbContext
  {
    public DbSet<UserImage> UserImages { get; set; } = null!;

    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {

    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
      var result = await base.SaveChangesAsync(cancellationToken);
      return result;
    }
  }
}