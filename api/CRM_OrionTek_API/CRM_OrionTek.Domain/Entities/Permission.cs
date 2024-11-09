using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static System.Collections.Specialized.BitVector32;

namespace CRM_OrionTek.Domain.Entities
{
    public class Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PermissionId { get; set; }
        public required string PermissionName { get; set; }
        public int SectionId { get; set; }

        [JsonIgnore]
        public Section Section { get; set; }
    }
}
