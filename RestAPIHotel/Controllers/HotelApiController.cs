using Microsoft.AspNetCore.Mvc;
using RestAPIHotel.Data;
using RestAPIHotel.Models.DTO;

namespace RestAPIHotel.Controllers
{
    [Route("api/hotelAPI")]
    [ApiController]
    public class HotelApiController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult <IEnumerable<roomDTO>> GetRooms()
        {
            return Ok(HotelStore.roomList);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<roomDTO> GetRoom(int id)
        {
            if(id== 0)
            {
                return BadRequest();
            }
            var room = HotelStore.roomList.FirstOrDefault(t => t.Id == id);
            if(room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<roomDTO> createRoom([FromBody]roomDTO room)
        {
            if(room == null)
            {
                return BadRequest(room);
            }
            if(room.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            room.Id = HotelStore.roomList.OrderByDescending(t => t.Id).FirstOrDefault().Id + 1;
            HotelStore.roomList.Add(room);

            return Ok(room);
        }
    }
}
