using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestHighSchool;
using TestHighSchool.Models;

namespace TestHighSchool.Controllers
{
    public class SubjectsController : Controller
    {
        private DB_TestHighSchoolEntities db = new DB_TestHighSchoolEntities();

        public ActionResult Index()
        {
            List<SubjectModel> list = new List<SubjectModel>();
            list = (from s in db.Subjects
                    where s.Active.Equals(1)
                    select new SubjectModel
                    {
                        Id = s.Id,
                        Name = s.Name.Trim(),
                        Active = s.Active == 1 ? true : false,
                    }).ToList();
            return View(list);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            SubjectModel model = new SubjectModel();
            model.Id = subject.Id;
            model.Name = subject.Name.Trim();
            model.Active = subject.Active == 1 ? true : false;
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Active")] SubjectModel subjectModel)
        {
            if (ModelState.IsValid)
            {
                Subject subject = new Subject();
                subject.Id = subjectModel.Id;
                subject.Name = subjectModel.Name;
                subject.Active = 1;
                db.Subjects.Add(subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subjectModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            SubjectModel model = new SubjectModel();
            model.Id = subject.Id;
            model.Name = subject.Name.Trim();
            model.Active = subject.Active == 1 ? true : false;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Active")] SubjectModel subjectModel)
        {
            if (ModelState.IsValid)
            {
                Subject subject = new Subject();
                subject.Id = subjectModel.Id;
                subject.Name = subjectModel.Name;
                subject.Active = 1;
                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subjectModel);
        }

        // GET: Subjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subject subject = db.Subjects.Find(id);
            subject.Active = 0;
            db.Entry(subject).State = EntityState.Modified;
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
