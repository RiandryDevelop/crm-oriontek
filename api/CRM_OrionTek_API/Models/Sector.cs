using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_OrionTek_API.Models
{
    [Table("Sector")]
    public class Sector
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SectorId { get; set; }

        [Column("SectorName")]
        public required string SectorName { get; set; }
        public int DistrictId { get; set; }
        public District? District { get; set; }
    }
}
