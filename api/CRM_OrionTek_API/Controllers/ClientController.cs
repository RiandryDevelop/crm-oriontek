using CRM_OrionTek_API.DTO;
using CRM_OrionTek_API.Models;
using CRM_OrionTek_API.Services.ClientService;
using Microsoft.AspNetCore.Mvc;

namespace CRM_OrionTek_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _client;
        public ClientController(IClientRepository client)
        {
            _client = client;
        }

        [HttpGet("GetClientPaginate")]
        public async Task<IActionResult> GetPaginate([FromQuery] int page, int size, string? queryData)
        {
            if (page < 0)
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
                    ClientName = clientDto.Name,
                    Locations = clientDto.ClientLocations?.Select(location => new Location
                    {
                        CountryId = location.CountryId,
                        ProvinceId = location.ProvinceId,
                        MunicipalityId = location.MunicipalityId,
                        DistrictId = location.DistrictId,
                        SectorId = location.SectorId,
                        CreateDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow
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
        public async Task<IActionResult> Update([FromBody] ClientDTO clientDto, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "The model is not valid" });
                }

                var client = new Client
                {
                    ClientName = clientDto.Name,
                    Locations = clientDto.ClientLocations?.Select(location => new Location
                    {
                        CountryId = location.CountryId,
                        ProvinceId = location.ProvinceId,
                        MunicipalityId = location.MunicipalityId,
                        DistrictId = location.DistrictId,
                        SectorId = location.SectorId,
                        UpdateDate = DateTime.UtcNow // Actualiza la fecha
                    }).ToList() ?? new List<Location>()
                };

                var data = await _client.Update(client, id);
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
