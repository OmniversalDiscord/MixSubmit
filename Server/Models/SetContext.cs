using Microsoft.EntityFrameworkCore;

namespace MixSubmit.Models;

public class SetContext : DbContext
{
    public DbSet<Set> Sets { get; set; }

    public SetContext(DbContextOptions<SetContext> options) : base(options)
    {
    }
    
    
}