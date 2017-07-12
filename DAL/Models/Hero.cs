using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("Hero", Schema = "rpg")]
    public class Hero
    {
        public Hero()
        {
            Abilities = new HashSet<Ability>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 2), Required]
        public string Name { get; set; }
        [MaxLength(4000)]
        public string Description { get; set; }
        public byte[] Image { get; set; }

        [MaxLength(128)]
         public string UserId { get; set; }
 
        public ICollection<Ability> Abilities { get; set; }
    }
}