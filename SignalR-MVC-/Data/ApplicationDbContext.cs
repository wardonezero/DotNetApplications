using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignalRMVC.Models;

namespace SignalRMVC.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<PrivateChat> PrivateChats { get; set; }
    }
}
