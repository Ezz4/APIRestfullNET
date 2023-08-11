using System.ComponentModel.DataAnnotations;

namespace RestAPIHotel.Models.DTO
{
    public class roomUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public int Occupancy { get; set; }
        [Required]
        public int Area { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        public string Url { get; set; }

        public string Amenity { get; set; }
    }
}
