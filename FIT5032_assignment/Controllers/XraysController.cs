﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using FIT5032_assignment.Models;

namespace FIT5032_assignment.Controllers
{
    public class XraysController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: Xrays
        [Authorize]
        public ActionResult Index()
        {
            return View(db.XraySet.ToList());
        }

        // GET: Xrays/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xray xray = db.XraySet.Find(id);
            if (xray == null)
            {
                return HttpNotFound();
            }
            return View(xray);
        }

        // GET: Xrays/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Xrays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,type,cost")] Xray xray)
        {
            if (ModelState.IsValid)
            {
                db.XraySet.Add(xray);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(xray);
        }

        // GET: Xrays/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xray xray = db.XraySet.Find(id);
            if (xray == null)
            {
                return HttpNotFound();
            }
            return View(xray);
        }

        // POST: Xrays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,type,cost")] Xray xray)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xray).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(xray);
        }

        // GET: Xrays/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xray xray = db.XraySet.Find(id);
            if (xray == null)
            {
                return HttpNotFound();
            }
            return View(xray);
        }

        // POST: Xrays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Xray xray = db.XraySet.Find(id);
            db.XraySet.Remove(xray);
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
