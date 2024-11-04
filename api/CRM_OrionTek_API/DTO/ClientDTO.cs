using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRM_OrionTek_API.DTO
{
    public class ClientDTO
    {
        [JsonIgnore]
        [Key]
        public int ClientId { get; set; }
        public required string Name { get; set; }

        public List<LocationDTO> ClientLocations { get; set; } = new List<LocationDTO>();
    }

    public class LocationDTO
    {
        [Required]
        public required string LocationName { get; set; }

        [Required]
        public required int CountryId { get; set; }

        [Required]
        public required int ProvinceId { get; set; }

        [Required]
        public required int MunicipalityId { get; set; }

        [Required]
        public required int DistrictId { get; set; }

        [Required]
        public required int SectorId { get; set; }
    }
}
