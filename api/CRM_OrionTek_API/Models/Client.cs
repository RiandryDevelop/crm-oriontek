using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CRM_OrionTek_API.Models
{
    [Table("Client")]
    public class Client
    {
        [Key]
        [Column("clientId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }

        [Column("ClientName")]
        [Required]
        public required string ClientName { get; set; } 
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public ICollection<Location>? Locations { get; set; }
    }
}
