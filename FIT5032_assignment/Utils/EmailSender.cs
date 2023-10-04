﻿using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FIT5032_Week08A.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";
        public void Send(string toEmail, string subject, string contents, string filePath)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("thomashktoaustralia@gmail.com", "FIT5032 Example Email User");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            // Check if filePath is not null and the file exists
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                // Read the file and attach it to the email
                var bytes = File.ReadAllBytes(filePath);
                var file = Convert.ToBase64String(bytes);
                msg.AddAttachment(Path.GetFileName(filePath), file);
            }

            var response = client.SendEmailAsync(msg);
        }


    }
}