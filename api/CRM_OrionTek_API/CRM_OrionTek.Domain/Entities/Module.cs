using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static System.Collections.Specialized.BitVector32;

namespace CRM_OrionTek.Domain.Entities
{
    public class Module
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModuleId { get; set; }
        public required string ModuleName { get; set; }

        public ICollection<Section>? Sections { get; set; }
    }
}
