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
    public class RatingsController : Controller
    {
        private SLTechsModelContainer db = new SLTechsModelContainer();

        // GET: Ratings
        public ActionResult Index()
        {
            var ratingSet = db.RatingSet.Include(r => r.CourseSet);
           
            List<int> reps = new List<int>();
            var courses = ratingSet.Select(x => x.CourseSet.Name).Distinct();
            foreach (var item in courses)
            {
                reps.Add(ratingSet.Count(x => x.CourseSet.Name == item));
            }
            ViewBag.COURSE = courses;
            ViewBag.REP = reps.ToList();
            return View(ratingSet.ToList());
        }

        // GET: Ratings/Details/5
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
            ViewBag.CourseId = new SelectList(db.CourseSet, "Id", "Name");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Comment,Rating_Stars,CourseId")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.RatingSet.Add(rating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.CourseSet, "Id", "Name", rating.CourseId);
            return View(rating);
        }

        // GET: Ratings/Edit/5
        [Authorize(Roles = "admin")]
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
            ViewBag.CourseId = new SelectList(db.CourseSet, "Id", "Name", rating.CourseId);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Comment,Rating_Stars,CourseId")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.CourseSet, "Id", "Name", rating.CourseId);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        [Authorize(Roles = "admin")]
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
