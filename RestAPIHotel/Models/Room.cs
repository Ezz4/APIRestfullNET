using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPIHotel.Models
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Details { get; set; }

        public double Rate { get; set; }

        public int Area { get; set; }

        public int Occupancy { get; set; }

        public string ImageUrl { get; set; }

        public string Url { get; set; }

        public string Amenity { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
