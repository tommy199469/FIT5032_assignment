using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_assignment.Models;

namespace FIT5032_assignment.Controllers
{
    public class GPsController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: GPs
        [Authorize]
        public ActionResult Index()
        {
            var gPSet = db.GPSet.Include(g => g.Xray);
            return View(gPSet.ToList());
        }

        // GET: GPs/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GP gP = db.GPSet.Find(id);
            if (gP == null)
            {
                return HttpNotFound();
            }
            return View(gP);
        }

        // GET: GPs/Create
        [Authorize(Roles ="Admin")]

        public ActionResult Create()
        {
            ViewBag.XrayId = new SelectList(db.XraySet, "Id", "type");
            return View();
        }

        // POST: GPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,ADDRESS,phone,email,XrayId")] GP gP)
        {
            if (ModelState.IsValid)
            {
                db.GPSet.Add(gP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.XrayId = new SelectList(db.XraySet, "Id", "type", gP.XrayId);
            return View(gP);
        }

        // GET: GPs/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GP gP = db.GPSet.Find(id);
            if (gP == null)
            {
                return HttpNotFound();
            }
            ViewBag.XrayId = new SelectList(db.XraySet, "Id", "type", gP.XrayId);
            return View(gP);
        }

        // POST: GPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,ADDRESS,phone,email,XrayId")] GP gP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.XrayId = new SelectList(db.XraySet, "Id", "type", gP.XrayId);
            return View(gP);
        }

        // GET: GPs/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GP gP = db.GPSet.Find(id);
            if (gP == null)
            {
                return HttpNotFound();
            }
            return View(gP);
        }

        // POST: GPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            GP gP = db.GPSet.Find(id);
            db.GPSet.Remove(gP);
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
