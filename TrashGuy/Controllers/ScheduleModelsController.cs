using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Models;
using TrashGuy.Models;

namespace TrashGuy.Controllers
{
    public class ScheduleModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ScheduleModels
        public ActionResult Index()
        {
            var scheduleModels = db.ScheduleModels.Include(s => s.ApplicationUser);
            return View(scheduleModels.ToList());
        }

        // GET: ScheduleModels/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Include(x => x.Schedule).Where(x => x.Id == id).FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            ScheduleModel scheduleModel = db.ScheduleModels.Find(user.Schedule.Id);
            if (scheduleModel == null)
            {
                return HttpNotFound();
            }
            return View(scheduleModel);
        }

        // GET: ScheduleModels/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: ScheduleModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ScheduleId,DefaultPickupDay")] ScheduleModel scheduleModel)
        {
            if (ModelState.IsValid)
            {
                db.ScheduleModels.Add(scheduleModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Users, "Id", "Name", scheduleModel.Id);
            return View(scheduleModel);
        }

        // GET: ScheduleModels/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleModel scheduleModel = db.ScheduleModels.Find(id);
            if (scheduleModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Name", scheduleModel.Id);
            return View(scheduleModel);
        }

        // POST: ScheduleModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DefaultPickupDay,ApplicationUser,VacationStartDate,VacationEndDate")] ScheduleModel scheduleModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scheduleModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "ScheduleModels", new { scheduleModel.Id });
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Name", scheduleModel.Id);
            return View(scheduleModel);
        }

        // GET: ScheduleModels/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleModel scheduleModel = db.ScheduleModels.Find(id);
            if (scheduleModel == null)
            {
                return HttpNotFound();
            }
            return View(scheduleModel);
        }

        // POST: ScheduleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ScheduleModel scheduleModel = db.ScheduleModels.Find(id);
            db.ScheduleModels.Remove(scheduleModel);
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
