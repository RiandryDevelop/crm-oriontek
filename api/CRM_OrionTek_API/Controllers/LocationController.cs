using CRM_OrionTek_API.Models;
using CRM_OrionTek_API.Services.LocationService;
using Microsoft.AspNetCore.Mvc;

namespace MeditodApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocation _location;
        public LocationController(ILocation location)
        {
            _location = location;
        }

        [HttpGet("GetLocationPaginate")]
        public async Task<IActionResult> GetPaginate([FromQuery] int page, int size, string? queryData)
        {

            if (page <= 0)
            {
                return BadRequest(new { Message = "The page must be greater than or equal to 1" });
            }

            var data = await _location.GetAllPaginated(page, size, queryData);

            return Ok(data);
        }

        [HttpGet("GetAllLocation")]
        public async Task<IActionResult> GetAll()
        {
            var results = await _location.GetAll();
            return Ok(results);
        }

        [HttpGet("GetOneLocation")]
        public async Task<IActionResult> GetOne([FromQuery] int id)
        {
            var results = await _location.GetOne(id);
            return Ok(results);
        }

        //[HttpGet("GetLocationByName")]
        //public async Task<IActionResult> GetByName([FromQuery] string name)
        //{
        //    var result = await _location.GetByname(name);
        //    return Ok(result);
        //}

        [HttpPost("CreateLocation")]
        public async Task<IActionResult> Create([FromBody] Location location)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "The model is not valid" });
                }

                var user = await _location.Create(location);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", error = ex.Message });
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Location location)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "The model is not valid" });
                }

                var user = await _location.Update(location);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", error = ex.Message });
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                var user = await _location.Delete(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", error = ex.Message });
            }
        }
    }
}
