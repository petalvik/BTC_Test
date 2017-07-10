using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using DAL.Models;

namespace BTC.ViewModels
{
    public class HeroViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public IEnumerable<string> AbilityNames { get; set; }

        public static implicit operator HeroViewModel(Hero hero)
        {
            var hvm = new HeroViewModel();
            hvm.Id = hero.Id;
            hvm.Name = hero.Name;
            hvm.Description = hero.Description;
            
            hvm.AbilityNames = hero.Abilities.Select(a => a.Name);

            return hvm;
        }

        public static implicit operator Hero(HeroViewModel hvm)
        {
            var hero = new Hero();
            hero.Id = hvm.Id;
            hero.Name = hvm.Name;
            hero.Description = hvm.Description;

            if (hvm.Image != null)
            {
                using (var target = new MemoryStream())
                {
                    hvm.Image.InputStream.CopyTo(target);
                    hero.Image = target.ToArray();
                }
            }
            return hero;
        }
    }
}