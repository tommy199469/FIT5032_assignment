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
    public class RatingsController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: Ratings
        [Authorize]

        public ActionResult Index()
        {
            var ratingSet = db.RatingSet.Include(r => r.User).Include(r => r.GP);
            return View(ratingSet.ToList());
        }

        // GET: Ratings/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.RatingSet.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // GET: Ratings/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.User_userId = new SelectList(db.UserSet, "userId", "FirstName");
            ViewBag.GPId = new SelectList(db.GPSet, "Id", "ADDRESS");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,User_userId,GPId")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.RatingSet.Add(rating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_userId = new SelectList(db.UserSet, "userId", "FirstName", rating.User_userId);
            ViewBag.GPId = new SelectList(db.GPSet, "Id", "ADDRESS", rating.GPId);
            return View(rating);
        }

        // GET: Ratings/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.RatingSet.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_userId = new SelectList(db.UserSet, "userId", "FirstName", rating.User_userId);
            ViewBag.GPId = new SelectList(db.GPSet, "Id", "ADDRESS", rating.GPId);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,User_userId,GPId")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_userId = new SelectList(db.UserSet, "userId", "FirstName", rating.User_userId);
            ViewBag.GPId = new SelectList(db.GPSet, "Id", "ADDRESS", rating.GPId);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.RatingSet.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Rating rating = db.RatingSet.Find(id);
            db.RatingSet.Remove(rating);
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
