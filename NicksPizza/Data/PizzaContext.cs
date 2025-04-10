using Microsoft.EntityFrameworkCore;

namespace NicksPizza.Data;

public class PizzaContext : DbContext
{
    public PizzaContext(DbContextOptions<PizzaContext> options)
        : base(options)
    {
    }
    public DbSet<NicksPizza.Models.Pizza>? Pizzas { get; set; }
}

