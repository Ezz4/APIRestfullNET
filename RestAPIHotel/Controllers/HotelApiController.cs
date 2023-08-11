using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPIHotel.Data;
using RestAPIHotel.Models;
using RestAPIHotel.Models.DTO;

namespace RestAPIHotel.Controllers
{
    [Route("api/hotelAPI")]
    [ApiController]
    public class HotelApiController : ControllerBase
    {

        private readonly ApplicationDbContext _db;

        private readonly IMapper _mapper;
        
        public HotelApiController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        //private readonly ILogger<HotelApiController> _logger;
        //public HotelApiController(ILogger<HotelApiController> logger)
        //{
        //    _logger = logger;
        //}


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async  Task<ActionResult<IEnumerable<roomDTO>>> GetRooms()
        {
           // _logger.LogInformation("Getting all rooms");

            IEnumerable<Room> roomList = await _db.Rooms.ToListAsync();
            return Ok(_mapper.Map<List<roomDTO>>(roomList));
        }

        [HttpGet("{id:int}", Name = "GetRoom")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<roomDTO>> GetRoom(int id)
        {
            if (id == 0)
            {
              //  _logger.LogError("Get room error with ID" + id);
                return BadRequest();
            }
            var room = await _db.Rooms.FirstOrDefaultAsync(t => t.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<roomDTO>(room));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<roomDTO>> createRoom([FromBody] roomCreateDTO createDTO)
        {

            if ( await _db.Rooms.FirstOrDefaultAsync(t => t.Name.ToLower() == createDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "room already exists!");
                return BadRequest(ModelState);
            }

            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }
           
            Room model = _mapper.Map<Room>(createDTO);

          

            await _db.Rooms.AddAsync(model);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetRoom", new { id = model.Id }, model);
        }

        [HttpDelete("{id:int}", Name = "DeleteRoom")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var room = await _db.Rooms.FirstOrDefaultAsync(t => t.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            _db.Rooms.Remove(room);
           await  _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}", Name ="UpdateRoom")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async  Task<IActionResult> UpdateRoom(int id, [FromBody] roomUpdateDTO updateDto)
        {
            if(updateDto == null || id != updateDto.Id)
            {
                return BadRequest();
            }

            //var room = _db.Rooms.FirstOrDefault(t => t.Id == id);
            //room.Name = roomDto.Name;
            //room.Area = roomDto.Area;
            //room.Occupancy = roomDto.Occupancy;

            Room model = _mapper.Map<Room>(updateDto);

            

            _db.Rooms.Update(model);
             await _db.SaveChangesAsync();
            return NoContent();

        }

        [HttpPatch("{id:int}", Name ="UpdatePartialRoom")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialRoom(int id, JsonPatchDocument<roomUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var room = await _db.Rooms.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

            roomUpdateDTO roomDto = new()
            {
                Amenity = room.Amenity,
                Details = room.Details,
                Id = room.Id,
                ImageUrl = room.ImageUrl,
                Url = room.Url,
                Name = room.Name,
                Occupancy = room.Occupancy,
                Rate = room.Rate,
                Area = room.Area
            };



            if(room == null)
            {
                return NotFound();
            }

            patchDTO.ApplyTo(roomDto, ModelState);

            Room model = new()
            {
                Amenity = room.Amenity,
                Details = room.Details,
                Id = room.Id,
                ImageUrl = room.ImageUrl,
                Url = room.Url,
                Name = room.Name,
                Occupancy = room.Occupancy,
                Rate = room.Rate,
                Area = room.Area
            };

            _db.Rooms.Update(model);
            await _db.SaveChangesAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

    }
}
