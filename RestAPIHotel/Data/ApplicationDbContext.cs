using Microsoft.EntityFrameworkCore;
using RestAPIHotel.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RestAPIHotel.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasData(
                new Room()
                {
                    Id = 1,
                    Name="Luxory suite",
                    Details="A very big room and a jacuzzi",
                    ImageUrl= "https://imagenes.forociudad.com/fotos/203185-casa-fea.jpg",
                    Occupancy = 5,
                    Rate= 200,
                    Area=550,
                    Amenity= "personal hygiene products",
                    Url = "Not valid for now",
                    CreatedDate = DateTime.Now

                },
                new Room
                {
                Id = 2,
                    Name = "Not so Luxory suite",
                    Details = "A very not so big room and a small jacuzzi",
                    ImageUrl = "https://st.depositphotos.com/71898554/60977/i/450/depositphotos_609775626-stock-photo-a-lot-of-garbage-in.jpg",
                    Occupancy = 5,
                    Rate = 180,
                    Area = 500,
                    Amenity = "personal hygiene products",
                    Url = "Not valid for now",
                    CreatedDate = DateTime.Now

                });
        }
    }
}
