using System.ComponentModel.DataAnnotations;

namespace RestAPIHotel.Models.DTO
{
    public class roomCreateDTO
    {

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Details { get; set; }

        public double Rate { get; set; }

        public int Occupancy { get; set; }

        public int Area { get; set; }

        public string ImageUrl { get; set; }

        public string Url { get; set; }

        public string Amenity { get; set; }
    }
}
