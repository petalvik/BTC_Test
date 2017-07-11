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
    public class AbilityController : Controller
    {
        private readonly IRepository<Ability> _repository;
        private readonly string _userId;

        public AbilityController(IRepository<Ability> repository)
        {
            _repository = repository;
            _userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }

        public ActionResult ShowImage(int? id)
        {
            var hero = _repository.GetById(id);

            return File(hero.Image, "image/jpg");
        }

        // GET: Ability
        public ActionResult Index(int? id)
        {
            if (id.HasValue)
                Session["id"] = id.Value;
            int heroId = (int)Session["id"];

            var abilityViewModels = _repository
                .Get(filter: a => a.HeroId == heroId)
                .Select(a => (AbilityViewModel)a);
            return View(abilityViewModels);
        }

        // GET: Ability/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ability = _repository.GetById(id);
            if (ability == null)
            {
                return HttpNotFound();
            }
            return View((AbilityViewModel)ability);
        }

        // GET: Ability/Create
        public ActionResult Create(int? id)
        {
            if (id.HasValue)
                Session["id"] = id.Value;
            int heroId = (int)Session["id"];            
            return View();
        }

        // POST: Ability/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Image,HeroId")] AbilityViewModel avm)
        {
            if (ModelState.IsValid)
            {
                var ability = (Ability)avm;
                ability.UserId = _userId;

                ability.HeroId = (int)Session["id"];

                _repository.Insert(ability);
                _repository.Save();
                
                return RedirectToAction("Index");
            }

            return View(avm);
        }

        // GET: Ability/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ability = _repository.GetById(id);
            if (ability == null)
            {
                return HttpNotFound();
            }
            return View((AbilityViewModel)ability);
        }

        // POST: Ability/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Image")] AbilityViewModel avm)
        {
            if (ModelState.IsValid)
            {
                var ability = (Ability)avm;
                ability.HeroId = (int)Session["id"];

                if (ability.Image != null)
                {
                    _repository.Update(ability);
                }
                else
                {
                    var oldAbility = _repository.GetById(ability.Id);
                    oldAbility.Name = ability.Name;
                    oldAbility.Description = ability.Description;

                    _repository.Update(oldAbility);
                }

                _repository.Save();

                return RedirectToAction("Index");
            }            
            return View(avm);
        }

        // GET: Ability/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ability = _repository.GetById(id);
            if (ability == null)
            {
                return HttpNotFound();
            }
            return View((AbilityViewModel)ability);
        }

        // POST: Ability/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ability = _repository.GetById(id);
            _repository.Delete(ability);
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
