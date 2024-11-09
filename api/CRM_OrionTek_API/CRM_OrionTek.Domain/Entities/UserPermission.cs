using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace CRM_OrionTek.Domain.Entities
{
    public class UserPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserPermissionId { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        public int PermissionId { get; set; }

        [JsonIgnore]
        public Permission? Permission { get; set; }

        public int? PermissionGroupId { get; set; }
        public PermissionGroup? PermissionGroup { get; private set; }
    }
}
