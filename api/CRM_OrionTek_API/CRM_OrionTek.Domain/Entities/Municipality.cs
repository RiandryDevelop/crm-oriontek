using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_OrionTek.Domain.Entities
{
    [Table("Municipality")]
    public class Municipality
    {
        [Column("MunicipalityId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int MunicipalityId { get; set; }

        [Column("MunicipalityName")]
        public required string MunicipalityName { get; set; }

        [Column("ProvinceId")]
        public int ProvinceId { get; set; }
        public Province? Province { get; set; }
    }
}
