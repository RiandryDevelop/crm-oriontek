using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_OrionTek.Domain.Entities
{
    [Table("Country")]
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId { get; set; }

        [Column("CountryName")]
        public required string CountryName { get; set; }
    }
}
