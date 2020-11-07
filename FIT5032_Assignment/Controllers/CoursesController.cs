using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_Assignment.Models;

namespace FIT5032_Assignment.Controllers
{
    public class CoursesController : Controller
    {
        private SLTechsModelContainer db = new SLTechsModelContainer();
        
        // GET: Courses
        public ActionResult Index()
        {
            var list = db.CourseSet.ToList();
            
            return View(list);
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.CourseSet.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {

            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Time,BeginDate,EndDate,Duration")] Course course)
        {
            int begin = 0;
            int end = 0;
            if (ModelState.IsValid)
            {
                db.CourseSet.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");

                //if (course.BeginDate.Length == 10 && course.BeginDate.Substring(4, 1) == "/"
                //    && course.BeginDate.Substring(7, 1) == "/")
                //    if (isInt(course.BeginDate.Substring(0, 1)) && isInt(course.BeginDate.Substring(1, 1))
                //        && isInt(course.BeginDate.Substring(2, 1)) && isInt(course.BeginDate.Substring(3, 1))
                //        && isInt(course.BeginDate.Substring(5, 1)) && isInt(course.BeginDate.Substring(6, 1))
                //        && isInt(course.BeginDate.Substring(8, 1)) && isInt(course.BeginDate.Substring(9, 1)))
                //        {
                //            begin = 1;


                //    }
                //if (course.EndDate.Length == 10 && course.EndDate.Substring(4, 1) == "/"
                //    && course.EndDate.Substring(7, 1) == "/")
                //    if (isInt(course.EndDate.Substring(0, 1)) && isInt(course.EndDate.Substring(1, 1))
                //        && isInt(course.EndDate.Substring(2, 1)) && isInt(course.EndDate.Substring(3, 1))
                //        && isInt(course.EndDate.Substring(5, 1)) && isInt(course.EndDate.Substring(6, 1))
                //        && isInt(course.EndDate.Substring(8, 1)) && isInt(course.EndDate.Substring(9, 1)))
                //    {
                //        end = 1;


                //    }

                //if (begin == 0)
                //{
                //    ViewBag.ErrorMessage = "Please enter valid date format as yyyy/MM/dd";




                //}
                //if (end == 0)
                //{
                //    ViewBag.ErrorMessage2 = "Please enter valid date format as yyyy/MM/dd";
                //}
                //if (begin == 1 && end == 1)
                //{
                //    db.CourseSet.Add(course);
                //    db.SaveChanges();
                //    return RedirectToAction("Index");
                //}
            }

            

            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.CourseSet.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Time,BeginDate,EndDate,Duration")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.CourseSet.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.CourseSet.Find(id);
            db.CourseSet.Remove(course);
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

        public bool isInt(string n) {

            if (n == "0" || n == "1" || n == "2" || n == "3" || n == "4" || n == "5" || n == "6" ||
                n == "7" || n == "8" || n == "9")
            {
                return true;
            }

            else
                return false;
        }
    }
}
