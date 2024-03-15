namespace MiBocataAPI.DB;

public class MBDBContext : DbContext
{
    public MBDBContext(DbContextOptions<MBDBContext> options) : base(options) { }

    public DbSet<Client> Client { get; set; }

    public DbSet<Shopkeeper> Shopkeeper { get; set; }

    public DbSet<Order> Order { get; set; }

    public DbSet<Store> Store { get; set; }

    public DbSet<Mibocata.Infrastructure.Data.Models.Product> Product { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    //    base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>()
                    .Ignore(b => b.Token);
        modelBuilder.Entity<Shopkeeper>()
                    .Ignore(b => b.Token);
    }
}