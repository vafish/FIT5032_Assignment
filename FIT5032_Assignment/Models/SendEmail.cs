using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FIT5032_Assignment.Models
{
    public class SendEmail
    {
        [Required(ErrorMessage = "Please enter the destination email address.")]
        [Display(Name = "To Email:")]
        public string ToEmail { get; set; }
        [Display(Name = "To Email2:")]
        public string ToEmail2 { get; set; }
        [Display(Name = "To Email3:")]
        public string ToEmail3 { get; set; }
        [Display(Name = "Subject:")]
        [Required(ErrorMessage = "Please enter the subject.")]
        public string Subject { get; set; }
        [Display(Name = "Contents:")]
        [Required(ErrorMessage = "Please enter the contents.")]
        public string Contents { get; set; }
        [Display(Name = "Select File Attachment:")]
        public HttpPostedFileBase Attachment { get; set; }
        [Display(Name = "From Email:")]
        //[Required(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        [Display(Name = "Password:")]
       // [Required(ErrorMessage = "Please enter password.")]
        public string Password { get; set; }
    }
}