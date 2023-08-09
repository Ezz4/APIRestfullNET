using RestAPIHotel.Models.DTO;

namespace RestAPIHotel.Data
{
    public class HotelStore
    {
        public static List<roomDTO> roomList = new List<roomDTO>
        {
            new roomDTO { Id = 1, Name="Pool view", Occupancy=4, Area=42},
            new roomDTO { Id = 2, Name= "Tower 1", Occupancy=2, Area=32}
        };
    }
}
