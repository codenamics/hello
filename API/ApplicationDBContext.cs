using System.Diagnostics.CodeAnalysis;
using API.Entity;
using Microsoft.EntityFrameworkCore;


namespace API
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
    
        }
        public DbSet<Board> Boards { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Card> Cards { get; set; }
     
    }
}