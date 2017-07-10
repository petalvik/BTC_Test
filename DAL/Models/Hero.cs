using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Hero
    {
        public Hero()
        {
            Abilities = new HashSet<Ability>();
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(128)]
        public string UserId { get; set; }
        [StringLength(100, MinimumLength = 2), Required]
        public string Name { get; set; }
        [MaxLength(4000)]
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public ICollection<Ability> Abilities { get; set; }
    }
}