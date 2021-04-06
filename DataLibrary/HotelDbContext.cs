using DataLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary
{
    public class HotelDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=HotelDB; Integrated Security=True");
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(user => user.Reservations).WithOne(reservation => reservation.User);
            modelBuilder.Entity<Room>().HasOne(room => room.Reservation).WithOne(reservation => reservation.Room);
            modelBuilder.Entity<Reservation>().HasMany(reservation => reservation.Clients).WithOne(client => client.Reservation);
        }

    }
}
