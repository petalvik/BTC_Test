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
        private readonly IRepository<Hero> _repository;
        private readonly string _userId;

        public HeroController(IRepository<Hero> repository)
        {
            _repository = repository;
            _userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }

        public ActionResult ShowImage(int? id)
        {
            var hero = _repository.GetById(id);

            return File(hero.Image, "image/jpg");
        }

        // GET: Hero
        public ActionResult Index()
        {
            var heroes = _repository.Get(includeProperties: "Abilities");
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
            var hero = _repository.GetById(id);
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
                hero.UserId = _userId;

                _repository.Insert(hero);
                _repository.Save();
                
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
            var hero = _repository.GetById(id);
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
                    _repository.Update(hero);
                }
                else
                {
                    var oldHero = _repository.GetById(hero.Id);
                    oldHero.Name = hero.Name;
                    oldHero.Description = hero.Description;

                    _repository.Update(oldHero);
                }
                _repository.Save();

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
            var hero = _repository.GetById(id);
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
            var hero = _repository.GetById(id);
            _repository.Delete(hero);
            _repository.Save();
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
