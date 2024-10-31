using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_OrionTek_API.Models
{
    public class Location
    {
        [Key]
        [Column("LocationId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationId { get; set; }

        [Column("LocationName")]
        public required string LocationName { get; set; }

        [Column("provinceName")]
        public required string ProvinceName { get; set; }
        
        [Column("MunicipalityName")]
        public required string MunicipalityName { get; set; }
        
        [Column("DistrictName")]
        public required string DistrictName { get; set; }
        
        [Column("sectorName")]
        public required string SectorName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
