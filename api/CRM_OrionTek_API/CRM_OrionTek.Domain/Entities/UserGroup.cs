using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace CRM_OrionTek.Domain.Entities
{
    public class UserGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserGroupId { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        public int GroupId { get; set; }

        [JsonIgnore]
        public Group? Group { get; set; }
    }
}
