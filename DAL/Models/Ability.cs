using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Ability
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(128)]
        public string UserId { get; set; }
        [StringLength(100, MinimumLength = 2), Required]
        public string Name { get; set; }
        [MaxLength(4000)]
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public int HeroId { get; set; }
        [ForeignKey("HeroId")]
        public Hero Hero { get; set; }
    }
}