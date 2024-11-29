using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_OrionTek_API.Models
{
    [Table("District")]
    public class District
    {
        [Column("DistrictId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DistrictId { get; set; }

        [Column("DistrictName")]
        public  required string DistrictName { get; set; }

        [Column("MunicipalityId")]
        public int MunicipalityId { get; set; }
        public Municipality? Municipality { get; set; }
    }
}
