using System.ComponentModel.DataAnnotations;

namespace RestAPIHotel.Models.DTO
{
    public class roomDTO
    {
        
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
       // public DateTime Date { get; set; }

        public int Occupancy { get; set; }

        public int Area { get; set; }
    }
}
