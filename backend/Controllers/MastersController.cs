using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using backend.DTOs.Masters;
using backend.Models.Masters;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/masters")]
    public class MastersController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public MastersController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // Colors
        [HttpGet("colors")]
        public IActionResult GetColors()
        {
            var colors = _db.MasterColors.ToList();
            var result = _mapper.Map<List<MasterColorDto>>(colors);
            return Ok(result);
        }

        [HttpGet("colors/{id}")]
        public IActionResult GetColorById(Guid id)
        {
            var color = _db.MasterColors.Find(id);
            if (color == null)
                return NotFound();

            var result = _mapper.Map<MasterColorDto>(color);
            return Ok(result);
        }

        [HttpPost("colors")]
        public IActionResult CreateColor(CreateMasterColorDto dto)
        {
            if (dto == null)
                return BadRequest();

            var color = _mapper.Map<MasterColor>(dto);
            _db.MasterColors.Add(color);
            _db.SaveChanges();

            var result = _mapper.Map<MasterColorDto>(color);
            return CreatedAtAction(nameof(GetColorById), new { id = color.Id }, result);
        }

        [HttpPut("colors/{id}")]
        public IActionResult UpdateColor(Guid id, UpdateMasterColorDto dto)
        {
            if (dto == null)
                return BadRequest();

            var color = _db.MasterColors.Find(id);
            if (color == null)
                return NotFound();

            _mapper.Map(dto, color);
            _db.SaveChanges();

            var result = _mapper.Map<MasterColorDto>(color);
            return Ok(result);
        }

        [HttpDelete("colors/{id}")]
        public IActionResult DeleteColor(Guid id)
        {
            var color = _db.MasterColors.Find(id);
            if (color == null)
                return NotFound();

            _db.MasterColors.Remove(color);
            _db.SaveChanges();

            return NoContent();
        }

        // Sizes
        [HttpGet("sizes")]
        public IActionResult GetSizes()
        {
            var sizes = _db.MasterSizes.ToList();
            var result = _mapper.Map<List<MasterSizeDto>>(sizes);
            return Ok(result);
        }

        [HttpGet("sizes/{id}")]
        public IActionResult GetSizeById(Guid id)
        {
            var size = _db.MasterSizes.Find(id);
            if (size == null)
                return NotFound();

            var result = _mapper.Map<MasterSizeDto>(size);
            return Ok(result);
        }

        [HttpPost("sizes")]
        public IActionResult CreateSize(CreateMasterSizeDto dto)
        {
            if (dto == null)
                return BadRequest();

            var size = _mapper.Map<MasterSize>(dto);
            _db.MasterSizes.Add(size);
            _db.SaveChanges();

            var result = _mapper.Map<MasterSizeDto>(size);
            return CreatedAtAction(nameof(GetSizeById), new { id = size.Id }, result);
        }

        [HttpPut("sizes/{id}")]
        public IActionResult UpdateSize(Guid id, UpdateMasterSizeDto dto)
        {
            if (dto == null)
                return BadRequest();

            var size = _db.MasterSizes.Find(id);
            if (size == null)
                return NotFound();

            _mapper.Map(dto, size);
            _db.SaveChanges();

            var result = _mapper.Map<MasterSizeDto>(size);
            return Ok(result);
        }

        [HttpDelete("sizes/{id}")]
        public IActionResult DeleteSize(Guid id)
        {
            var size = _db.MasterSizes.Find(id);
            if (size == null)
                return NotFound();

            _db.MasterSizes.Remove(size);
            _db.SaveChanges();

            return NoContent();
        }
    }
}