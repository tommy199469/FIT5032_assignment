using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace FIT5032_Week08A.Models
{
    public class SendEmailViewModel
    {

        [Required(ErrorMessage = "Please enter a subject.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter the contents")]
        public string Contents { get; set; }

        // Property to store selected recipient IDs
        public List<string> SelectedRecipients { get; set; }
    }
}
