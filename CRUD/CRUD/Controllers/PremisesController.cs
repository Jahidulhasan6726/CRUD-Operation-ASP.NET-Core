using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Controllers
{
    public class PremisesController : Controller
    {
        private readonly InstituteDbContext db;
        public PremisesController(InstituteDbContext db)
        {
            this.db = db;
        }
        public async Task <IActionResult> Index()
        {
            return View(db.Premises.Include(x=>x.Institute).ToList());
        }
        public IActionResult Create()
        {
            ViewBag.institutes = db.Institutes.ToList();
            return View();
        }


        [HttpPost]

        public IActionResult Create(Premise p)
        {
            if (ModelState.IsValid)
            {
                db.Premises.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }
        public IActionResult Edit(int? id)
        {
            var premises = db.Premises.Find(id);
            if(premises == null)
            {
                return BadRequest();
            }
            ViewBag.institues = db.Institutes.ToList();
            return View(premises);
        }
        [HttpPost]
        public IActionResult Edit(int?id,Premise premise)
        {
            if (id != premise.PremiseId)
            {
                return NotFound();
            }
            ViewBag.institutes = db.Institutes.ToList();
            if (ModelState.IsValid)
            {
                db.Premises.Update(premise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(premise);
        }
       [HttpPost,ActionName("Delete")]
       public IActionResult DoDelete(int id) 
        {
            var premises = db.Premises.Find(id);
            if(premises != null)
            {
                db.Entry(premises).State = EntityState.Deleted;
                
            }
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
