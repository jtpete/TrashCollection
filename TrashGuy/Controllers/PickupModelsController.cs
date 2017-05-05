using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashGuy.Models;

namespace TrashGuy.Controllers
{
    public class PickupModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PickupModels
        public ActionResult Index()
        {
            var pickupModels = db.PickupModels.Include(p => p.AppUser);

            ICollection<PickupModel> billableDates = pickupModels.Where(d => d.PickupDate.Year == DateTime.Now.Year && d.PickupDate.Month == DateTime.Now.Month).ToList();
            double MonthsBill = billableDates.Count() * 20;
            MonthsBill = Math.Round(MonthsBill);
            ViewBag.Bill = MonthsBill;
            return View(billableDates.ToList());
        }

        // GET: PickupModels/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickupModel pickupModel = db.PickupModels.Find(id);
            if (pickupModel == null)
            {
                return HttpNotFound();
            }
            return View(pickupModel);
        }

        // GET: PickupModels/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: PickupModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PickupId,Id,PickupDate")] PickupModel pickupModel)
        {
            if (ModelState.IsValid)
            {
                db.PickupModels.Add(pickupModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Users, "Id", "Name", pickupModel.Id);
            return View(pickupModel);
        }

        // GET: PickupModels/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickupModel pickupModel = db.PickupModels.Find(id);
            if (pickupModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Name", pickupModel.Id);
            return View(pickupModel);
        }

        // POST: PickupModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PickupId,Id,PickupDate")] PickupModel pickupModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pickupModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Name", pickupModel.Id);
            return View(pickupModel);
        }

        // GET: PickupModels/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickupModel pickupModel = db.PickupModels.Find(id);
            if (pickupModel == null)
            {
                return HttpNotFound();
            }
            return View(pickupModel);
        }

        // POST: PickupModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PickupModel pickupModel = db.PickupModels.Find(id);
            db.PickupModels.Remove(pickupModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
