using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlacementHelper.Models;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace PlacementHelper
{
    public class LogsController : Controller
    {
        private PlacementLoggerDBEntities db = new PlacementLoggerDBEntities();

        // GET: Logs
        public ActionResult Index()
        {
           
            if (User.IsInRole("Student"))
            {
                string currentUserId = User.Identity.GetUserId();
                var logs = db.Logs.Where(uo => uo.UserID == currentUserId).OrderBy(l => l.StartDate);
                return View(logs.ToList());

            }
            else
            {
                var logs = db.Logs.Include(l => l.AspNetUser).OrderBy(l => l.AspNetUser.Email).ThenBy(l => l.StartDate);
                return View(logs.ToList());
            }
                
           
           
        }

        // GET: Logs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        // GET: Logs/Create
        public ActionResult Create()
        {
            // ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            Log i = new Log();
            
            i.UserID = User.Identity.GetUserId();
            return View(i);
        }

        // POST: Logs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StartDate,EndDate,Description,UserId")] Log log)
        {
            if (ModelState.IsValid)
            {
                //log.UserID  = "e64d821b-4b35-40f0-b9b6-43cdf140b0c4";
                log.Id = Guid.NewGuid();
                db.Logs.Add(log);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", log.UserID);
            return View(log);
        }

        // GET: Logs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", log.UserID);
            return View(log);
        }

        // POST: Logs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartDate,EndDate,Description,UserID")] Log log)
        {
            if (ModelState.IsValid)
            {
                db.Entry(log).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", log.UserID);
            return View(log);
        }

        // GET: Logs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        // POST: Logs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Log log = db.Logs.Find(id);
            db.Logs.Remove(log);
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
