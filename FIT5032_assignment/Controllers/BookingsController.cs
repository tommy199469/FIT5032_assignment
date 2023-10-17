using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_assignment.Models;
using Microsoft.AspNet.Identity;
using Spire.Xls;
using Newtonsoft.Json;

namespace FIT5032_assignment.Controllers
{
    public class BookingsController : Controller
    {
        private Model1Container db = new Model1Container();

        // GET: Bookings
        [Authorize]
        public ActionResult Index()
        {
            var bookingSet = db.BookingSet.Include(b => b.GP);
            return View(bookingSet.ToList());
        }

        // GET: Bookings/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.BookingSet.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.GPId = new SelectList(db.GPSet, "Id", "ADDRESS");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,GPId,bookingDateTime")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                var existingBooking = db.BookingSet.FirstOrDefault(b => b.bookingDateTime == booking.bookingDateTime && b.GPId == booking.GPId);
                if (booking.bookingDateTime.Date < DateTime.Today)
                {
                    ModelState.AddModelError("bookingDateTime", "You only allow to book the date after today.");
                }

                if (existingBooking != null)
                {
                    // If a booking is found, we want to inform the user and return the view.
                    ModelState.AddModelError(string.Empty, "The GP was occupyied, please choose another date.");
                }
                else
                {
                    // If no booking is found, proceed with creating the new booking.
                    booking.userId = User.Identity.GetUserId();
                    db.BookingSet.Add(booking);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            // If we reach here, something failed, redisplay form.
            ViewBag.GPId = new SelectList(db.GPSet, "Id", "ADDRESS", booking.GPId);
            return View(booking);
        }


        // GET: Bookings/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.BookingSet.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.GPId = new SelectList(db.GPSet, "Id", "ADDRESS", booking.GPId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,GPId,bookingDateTime")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GPId = new SelectList(db.GPSet, "Id", "ADDRESS", booking.GPId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.BookingSet.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.BookingSet.Find(id);
            db.BookingSet.Remove(booking);
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

        [Authorize]
        public ActionResult ExportToCsv()
        {
            var bookingSet = db.BookingSet.Include(b => b.GP);
            var bookings = bookingSet.ToList();

            // Create a new workbook
            Workbook workbook = new Workbook();
            Worksheet worksheet = workbook.Worksheets[0];

            // Set the header row
            worksheet.Range["A1"].Text = "Booking ID";
            worksheet.Range["B1"].Text = "GP ID";
            worksheet.Range["C1"].Text = "Booking Date";

            int row = 2;
            foreach (var booking in bookings)
            {
                worksheet.Range[row, 1].Value = booking.Id.ToString();
                worksheet.Range[row, 2].Value = booking.GPId.ToString();
                worksheet.Range[row, 3].Value = booking.bookingDateTime.ToString("yyyy-MM-dd");
                row++;
            }

            var textFileName = "bookings.csv";
            var textFilePath = Server.MapPath("~/App_Data/" + textFileName);
            workbook.SaveToFile(textFilePath, FileFormat.CSV);

            Response.ContentType = "text/csv";
            Response.AddHeader("content-disposition", $"attachment; filename={textFileName}");

            return File(textFilePath, "text/csv");
        }

        [Authorize]
        public ActionResult ExportToJson()
        {
            var bookingSet = db.BookingSet.Include(b => b.GP);
            var bookings = bookingSet.ToList();

            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string json = JsonConvert.SerializeObject(bookings, Formatting.Indented, jsonSettings);


            var jsonName = "bookings.json";
            var jsonFilePath = Server.MapPath("~/Export/" + jsonName);
            System.IO.File.WriteAllText(jsonFilePath, json);

            Response.ContentType = "application/json";
            Response.AddHeader("content-disposition", $"attachment; filename={jsonName}");

            return File(jsonFilePath, "application/json");
        }
    }
}
