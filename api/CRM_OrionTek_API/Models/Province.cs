using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_OrionTek_API.Models
{
    [Table("Province")]
    public class Province
    {
        [Column("ProvinceId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProvinceId { get; set; }

        [Column("ProvinceName")]
        public required string ProvinceName { get; set; }

    }
}
