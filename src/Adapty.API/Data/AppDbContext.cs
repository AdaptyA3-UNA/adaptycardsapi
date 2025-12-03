using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Adapty.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Adapty.API.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions options) : base(options) {}

        // Registre seus Models aqui
        public DbSet<Deck> Decks { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Study> Studies { get; set; }
    }
}