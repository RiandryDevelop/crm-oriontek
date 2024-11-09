using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace CRM_OrionTek.Domain.Entities
{
    public class PermissionGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PermissionGroupId { get; set; }
        public int GroupId { get; set; }

        [JsonIgnore]
        public Group? Group { get; set; }

        public int PermissionId { get; set; }
        public Permission? Permission { get; set; }
    }
}
