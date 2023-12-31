﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_assignment.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace FIT5032_assignment.Controllers
{
    public class RatingsController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: Ratings
        [Authorize]

        public ActionResult Index()
        {
            var ratingSet = db.RatingSet.Include(r => r.GP);

            double averageScore = ratingSet.Average(r => r.score);

            var gpData = new List<Tuple<string, double>>(); 

            foreach (var gp in db.GPSet.ToList())
            {
                var ratings = ratingSet.Where(r => r.GPId == gp.Id).ToList(); 
                double totalScore = 0;
                int count = 0;

                foreach (var rating in ratings)
                {
                    if (rating.score != null)
                    {
                        totalScore += (double)rating.score;
                        count++;
                    }
                }

                double gpAverage = (count > 0) ? totalScore / count : 0; 
                gpData.Add(Tuple.Create(gp.ADDRESS, gpAverage));
            }


            ViewBag.AverageScore = averageScore;
            ViewBag.GPData = gpData;

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
            ViewBag.GPId = new SelectList(db.GPSet, "Id", "ADDRESS");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,GPId,score")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                rating.userId = User.Identity.GetUserId();
                db.RatingSet.Add(rating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            ViewBag.GPId = new SelectList(db.GPSet, "Id", "ADDRESS", rating.GPId);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,GPId,score")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
