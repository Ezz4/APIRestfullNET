using Microsoft.EntityFrameworkCore;
using RestAPIHotel.Models;

namespace RestAPIHotel.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
    }
}
