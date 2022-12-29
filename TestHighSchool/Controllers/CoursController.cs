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
    public class CoursController : Controller
    {
        private DB_TestHighSchoolEntities db = new DB_TestHighSchoolEntities();

        public ActionResult Index()
        {
            //var courses = db.Courses.Include(c => c.Student).Include(c => c.Teacher).ToList();
            List<CourseModel> list = new List<CourseModel>();
            list = (from c in db.Courses
                        //where c.Active.Equals(1)
                    from a in db.Students.Where(a => a.Id.Equals(c.StudentId)).DefaultIfEmpty()
                    from t in db.Teachers.Where(t => t.Id.Equals(c.TeacherId)).DefaultIfEmpty()
                    from s in db.Subjects.Where(s=>s.Id.Equals(t.SubjectId)).DefaultIfEmpty()
                    select new CourseModel
                    {
                        Id = c.Id,
                        TeacherId = t.Id,
                        Teacher = t.Name.Trim(),
                        StudentId = a.Id,
                        Student = a.Name.Trim(),
                        Subject = s.Name.Trim(),
                        Evaluation1 = c.Evaluation1,
                        Evaluation2 = c.Evaluation2,
                        Evaluation3 = c.Evaluation3,
                        FinalAverage = c.FinalAverage ?? 0,
                    }).ToList();
            return View(list);
        }

        // GET: Cours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // GET: Cours/Create
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name");
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name");
            return View();
        }

        // POST: Cours/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TeacherId,StudentId,Evaluation1,Evaluation2,Evaluation3,FinalAverage")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(cours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", cours.StudentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", cours.TeacherId);
            return View(cours);
        }

        // GET: Cours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", cours.StudentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", cours.TeacherId);
            return View(cours);
        }

        // POST: Cours/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TeacherId,StudentId,Evaluation1,Evaluation2,Evaluation3,FinalAverage")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", cours.StudentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", cours.TeacherId);
            return View(cours);
        }

        // GET: Cours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // POST: Cours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cours cours = db.Courses.Find(id);
            db.Courses.Remove(cours);
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
