using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_OrionTek_API.Models
{
    [Table("Location")]
    public class Location
    {
        [Key]
        [Column("LocationId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationId { get; set; }

        [Column("CountryId")]
        public required int CountryId { get; set; }
        public Country? Country { get; set; }

        [Column("provinceId")]
        public required int ProvinceId { get; set; }
        public Province? Province { get; set; }
        
        [Column("MunicipalityId")]
        public required int MunicipalityId { get; set; }
        public Municipality? Municipality { get; set; }


        [Column("DistrictId")]
        public required int DistrictId { get; set; }
        public District? District { get; set; }
        
        [Column("sectorId")]
        public required int SectorId { get; set; }
        public Sector? Sector { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
