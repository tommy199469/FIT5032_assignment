using FIT5032_assignment.Models;
using FIT5032_Week08A.Models;
using FIT5032_Week08A.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5032_assignment.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
     

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles ="Admin")]
        public ActionResult Send_Email()
        {
            var db = new ApplicationDbContext();
            var Users = db.Users;

            ViewBag.users = Users.ToList();
            return View(new SendEmailViewModel());
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Send_Email(SendEmailViewModel model, HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    List<string> selectedRecipientIds = model.SelectedRecipients;
                    String subject = model.Subject;
                    String contents = model.Contents;
                    string filePath = "";

                    // Check if a file was uploaded
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        // Process the uploaded file
                        string fileName = Path.GetFileName(postedFile.FileName);
                        filePath = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                        postedFile.SaveAs(filePath);
                        // You can now use the 'filePath' for further processing, such as attaching it to the email.
                    }

                    EmailSender es = new EmailSender();
                    var db = new ApplicationDbContext();
                    var Users = db.Users;
                    ViewBag.users = Users.ToList();

                    foreach (string recipientId in selectedRecipientIds)
                    {
                        // Retrieve the email address for the recipient using recipientId
                        // You might need to fetch it from your database or some other source
                        var user = db.Users.Find(recipientId);

                        // Send the email to the recipient
                        es.Send(user.Email, subject, contents, filePath);
                    }

                    ViewBag.Result = "Email has been send.";
                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View(new SendEmailViewModel());
                }
            }

            return View(new SendEmailViewModel());
        }
    }
}