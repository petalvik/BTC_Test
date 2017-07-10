using DAL.Models;
using System.IO;
using System.Web;

namespace BTC.ViewModels
{
    public class AbilityViewModel
    {
        public int Id { get; set; }
        public int HeroId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase Image { get; set; }

        public static implicit operator AbilityViewModel(Ability ability)
        {
            var avm = new AbilityViewModel();
            avm.Id = ability.Id;
            avm.HeroId = ability.HeroId;
            avm.Name = ability.Name;
            avm.Description = ability.Description;

            return avm;
        }

        public static implicit operator Ability(AbilityViewModel avm)
        {
            var ability = new Ability();
            ability.Id = avm.Id;
            ability.HeroId = avm.HeroId;
            ability.Name = avm.Name;
            ability.Description = avm.Description;

            if (avm.Image != null)
            {
                using (var target = new MemoryStream())
                {
                    avm.Image.InputStream.CopyTo(target);
                    ability.Image = target.ToArray();
                }
            }
            return ability;
        }
    }
}