using System;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL.DB
{
    public class UsersAndAwardsContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Award> Awards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("InMemoryDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAward>().HasKey(ua => new {ua.IdAward, ua.IdUser});
        }
    }
}