using RestAPIHotel.Models.DTO;

namespace RestAPIHotel.Data
{
    public class HotelStore
    {
        public static List<roomDTO> roomList = new List<roomDTO>
        {
            new roomDTO { Id = 1, Name="Pool view"},
            new roomDTO { Id = 2, Name= "Tower 1"}
        };
    }
}
