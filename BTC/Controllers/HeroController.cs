using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNet.Identity;
using BTC.ViewModels;

namespace BTC.Controllers
{
    [Authorize]
    public class HeroController : Controller
    {
        private IRepository<Hero> repository;
        private string userId;

        public HeroController(IRepository<Hero> repository)
        {
            this.repository = repository;
            userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }

        public ActionResult ShowImage(int? id)
        {
            var hero = repository.GetById(id);

            return File(hero.Image, "image/jpg");
        }

        // GET: Hero
        public ActionResult Index()
        {
            var heroes = repository.Get(includeProperties: "Abilities");
            var heroViewModels = heroes.Select(h => (HeroViewModel)h);
            return View(heroViewModels);
        }

        // GET: Hero/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var hero = repository.GetById(id);
            if (hero == null)
            {
                return HttpNotFound();
            }
            return View((HeroViewModel)hero);
        }

        // GET: Hero/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hero/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Image")] HeroViewModel hvm)
        {
            if (ModelState.IsValid)
            {
                Hero hero = (Hero)hvm;
                hero.UserId = userId;

                repository.Insert(hero);
                repository.Save();
                
                return RedirectToAction("Index");
            }

            return View(hvm);
        }

        // GET: Hero/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var hero = repository.GetById(id);
            if (hero == null)
            {
                return HttpNotFound();
            }
            return View((HeroViewModel)hero);
        }

        // POST: Hero/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Image")] HeroViewModel hvm)
        {
            if (ModelState.IsValid)
            {
                var hero = (Hero)hvm;

                if (hero.Image != null)
                {
                    repository.Update(hero);
                }
                else
                {
                    var oldHero = repository.GetById(hero.Id);
                    oldHero.Name = hero.Name;
                    oldHero.Description = hero.Description;

                    repository.Update(oldHero);
                }
                repository.Save();

                return RedirectToAction("Index");
            }
            return View(hvm);
        }

        // GET: Hero/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var hero = repository.GetById(id);
            if (hero == null)
            {
                return HttpNotFound();
            }
            return View((HeroViewModel)hero);
        }

        // POST: Hero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var hero = repository.GetById(id);
            repository.Delete(hero);
            repository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
