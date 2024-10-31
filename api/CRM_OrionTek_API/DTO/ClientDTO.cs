    using System.ComponentModel.DataAnnotations.Schema;
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
            public required string LocationName { get; set; }
            public required string ProvinceName { get; set; }
            public required string MunicipalityName { get; set; }
            public required string DistrictName { get; set; }
            public required string SectorName { get; set; }
        }
    }
