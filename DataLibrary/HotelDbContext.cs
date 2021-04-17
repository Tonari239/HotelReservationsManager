using DataLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DataLibrary
{
    public class HotelDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }


        public HotelDbContext(DbContextOptions<HotelDbContext> options) :base(options)
        {

        }
        public HotelDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = HotelDB; Integrated Security = True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(user => user.Reservations).WithOne(reservation => reservation.User);
            modelBuilder.Entity<Room>().HasOne(room => room.Reservation).WithOne(reservation => reservation.Room).HasForeignKey<Reservation>(t => t.RoomId);
            modelBuilder.Entity<Reservation>().HasMany(reservation => reservation.Clients).WithOne(client => client.Reservation);
            
        }

    }
}
