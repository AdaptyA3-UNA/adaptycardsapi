using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Adapty.API.Models;
using Adapty.API.DTOs;

namespace Adapty.API.Data
{
    public class AppDbContext : DbContext
    {
         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Registre seus Models aqui
        public DbSet<User> Users { get; set; }
        public DbSet<Deck> Deck { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<Study> Studies { get; set; }
    }
}