using CRM_OrionTek_API.DTO;
using CRM_OrionTek_API.Models;
using CRM_OrionTek_API.Services.ClientService;
using Microsoft.AspNetCore.Mvc;

namespace MeditodApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClient _client;
        public ClientController(IClient client)
        {
            _client = client;
        }

        [HttpGet("GetClientPaginate")]
        public async Task<IActionResult> GetPaginate([FromQuery] int page, int size, string? queryData)
        {

            if (page <= 0)
            {
                return BadRequest(new { Message = "The page must be greater than or equal to 1" });
            }

            var data = await _client.GetAllPaginated(page, size, queryData);

            return Ok(data);
        }

        [HttpGet("GetOneClient")]
        public async Task<IActionResult> GetOne([FromQuery] int id)
        {
            var results = await _client.GetOne(id);
            return Ok(results);
        }

        [HttpGet("GetClientByName")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var result = await _client.GetByname(name);
            return Ok(result);
        }

        [HttpPost("CreateClient")]
        public async Task<IActionResult> Create([FromBody] ClientDTO clientDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "The model is not valid" });
                }
                var client = new Client
                {
                    Name = clientDto.Name,
                    Locations = clientDto.ClientLocations?.Select(location => new Location
                    {
                        DistrictName = location.DistrictName,
                        SectorName = location.SectorName,
                        MunicipalityName = location.MunicipalityName,
                        LocationName = location.LocationName,
                        ProvinceName = location.ProvinceName,
                    }).ToList() ?? new List<Location>()
                };


                var data = await _client.Create(client);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", error = ex.Message });
            }
        }

        [HttpPut("UpdateClient")]
        public async Task<IActionResult> Update([FromBody] ClientDTO clientDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "The model is not valid" });
                }

                var client = new Client
                {
                    Name = clientDto.Name,
                    Locations = clientDto.ClientLocations?.Select(location => new Location
                    {
                        DistrictName = location.DistrictName,
                        SectorName = location.SectorName,
                        MunicipalityName = location.MunicipalityName,
                        LocationName = location.LocationName,
                        ProvinceName = location.ProvinceName,
                    }).ToList() ?? new List<Location>()
                };



                var data = await _client.Update(client);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", error = ex.Message });
            }
        }

        [HttpDelete("DeleteClient")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                var data = await _client.Delete(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", error = ex.Message });
            }
        }

    }
}
