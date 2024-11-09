using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CRM_OrionTek.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public required string Name { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public int? CreatedBy { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public ICollection<UserPermission>? UserPermissions { get; set; }
        public ICollection<UserGroup>? UserGroups { get; set; }
        [NotMapped]
        public string? CreatorName { get; set; }

    }
}
