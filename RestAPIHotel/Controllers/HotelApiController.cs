using Microsoft.AspNetCore.JsonPatch;
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
        public ActionResult<IEnumerable<roomDTO>> GetRooms()
        {
            return Ok(HotelStore.roomList);
        }

        [HttpGet("{id:int}", Name = "GetRoom")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<roomDTO> GetRoom(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var room = HotelStore.roomList.FirstOrDefault(t => t.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<roomDTO> createRoom([FromBody] roomDTO room)
        {

            if (HotelStore.roomList.FirstOrDefault(t => t.Name.ToLower() == room.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "room already exists!");
                return BadRequest(ModelState);
            }

            if (room == null)
            {
                return BadRequest(room);
            }
            if (room.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            room.Id = HotelStore.roomList.OrderByDescending(t => t.Id).FirstOrDefault().Id + 1;
            HotelStore.roomList.Add(room);

            return CreatedAtRoute("GetRoom", new { id = room.Id }, room);
        }

        [HttpDelete("{id:int}", Name = "DeleteRoom")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteRoom(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var room = HotelStore.roomList.FirstOrDefault(t => t.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            HotelStore.roomList.Remove(room);
            return NoContent();
        }

        [HttpPut("{id:int}", Name ="UpdateRoom")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateRoom(int id, [FromBody] roomDTO roomDto)
        {
            if(roomDto == null || id != roomDto.Id)
            {
                return BadRequest();
            }
            var room = HotelStore.roomList.FirstOrDefault(t => t.Id == id);

            room.Name = roomDto.Name;
            room.Area = roomDto.Area;
            room.Occupancy = roomDto.Occupancy;

            return NoContent();

        }

        [HttpPatch("{id:int}", Name ="UpdatePartialRoom")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialRoom(int id, JsonPatchDocument<roomDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var room = HotelStore.roomList.FirstOrDefault(t => t.Id == id);
            
            if(room == null)
            {
                return NotFound();
            }

            patchDTO.ApplyTo(room, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

    }
}
