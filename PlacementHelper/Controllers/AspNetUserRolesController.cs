using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlacementHelper.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace PlacementHelper.Controllers
{
    public class AspNetUserRolesController : Controller
    {
        private PlacementLoggerDBEntities db = new PlacementLoggerDBEntities();
        private ApplicationUserManager _userManager;

        // GET: AspNetUserRoles

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))

            {
                var aspNetUserRoles = db.AspNetUserRoles.Include(a => a.AspNetRole).Include(a => a.AspNetUser);
                return View(aspNetUserRoles.ToList());
            }
            else
            {
                return RedirectToAction("index", "Home");
            }
        }


        // GET: AspNetUserRoles/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: AspNetUserRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,RoleId,description")] AspNetUserRole aspNetUserRole)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUserRoles.Add(aspNetUserRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", aspNetUserRole.RoleId);
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserRole.UserId);
            return View(aspNetUserRole);
        }


        // GET: AspNetUserRoles/Delete/5
        public ActionResult Delete(string RoleName, string UserId)
        {
            UserManager.RemoveFromRole(UserId, RoleName);
          //  AspNetUserRole aspNetUserRole = db.AspNetUserRoles.Find(id);
           // if (aspNetUserRole == null)
          //  {
            //    return HttpNotFound();
          //  }
            return RedirectToAction("index", "AspNetUserRoles");
        }

        // POST: AspNetUserRoles/Delete/5
      
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
