using FIT5032_Assignment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace FIT5032_Assignment.Controllers
{
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


        public ActionResult SendEmail()
        {
            return View(new SendEmail());
        }

        [HttpPost]

        public ActionResult SendEmail(SendEmail objModelMail, HttpPostedFileBase fileUploader)

        {

            if (ModelState.IsValid)

            {

                string from = "hongyuchen@gmail.com"; //example:- sourabh9303@gmail.com

                using (MailMessage mail = new MailMessage(from, objModelMail.ToEmail))

                {

                    mail.Subject = objModelMail.Subject;

                    mail.Body = objModelMail.Contents;

                    if (fileUploader != null)

                    {

                        string fileName = Path.GetFileName(fileUploader.FileName);

                        mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));

                    }

                    mail.IsBodyHtml = false;

                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.EnableSsl = true;

                    NetworkCredential networkCredential = new NetworkCredential(from, "Chy960710");

                    smtp.UseDefaultCredentials = true;

                    smtp.Credentials = networkCredential;

                    smtp.Port = 587;

                    smtp.Send(mail);

                    ViewBag.Message = "Sent";

                    return View("Index", objModelMail);

                }

            }

            else

            {

                return View();

            }

        }

    }



        //public void sendEmails(SendEmail model, string emailadd)
        //{
        //    MailMessage mm = new MailMessage(model.Email, emailadd);


        //    mm.Subject = model.Subject;
        //    mm.Body = model.Contents;
        //    if (model.Attachment != null)
        //    {
        //        if (model.Attachment.ContentLength > 0)
        //        {
        //            string fileName = Path.GetFileName(model.Attachment.FileName);
        //            mm.Attachments.Add(new Attachment(model.Attachment.InputStream, fileName));
        //        }
        //    }
        //    mm.IsBodyHtml = true;
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = "smtp.gmail.com";
        //    NetworkCredential NetworkCred = new NetworkCredential(model.Email, model.Password);
        //    smtp.EnableSsl = true;
        //    smtp.UseDefaultCredentials = false;
        //    smtp.Credentials = NetworkCred;
        //    smtp.Port = 587;
        //    try
        //    {
        //        smtp.Send(mm);
        //        ViewBag.Message = "Email sent!";
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
        //[HttpPost]
        //public ActionResult SendEmail(SendEmail model)
        //{

        //    sendEmails(model, model.ToEmail);
        //    if (model.ToEmail2 != null)
        //        sendEmails(model, model.ToEmail2);
        //    if (model.ToEmail3 != null)
        //        sendEmails(model, model.ToEmail3);
        //    return View();
        //}
    //}
}