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
    public class StudentsController : Controller
    {
        private DB_TestHighSchoolEntities db = new DB_TestHighSchoolEntities();

        public ActionResult Index()
        {
            List<StudentModel> list = new List<StudentModel>();
            list = (from s in db.Students
                    where s.Active.Equals(1)
                    select new StudentModel
                    {
                        Id = s.Id,
                        Name = s.Name.Trim(),
                        LastName = s.LastName.Trim(),
                        Tel = s.Tel.Trim(),
                        Email = s.Email.Trim(),
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
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            StudentModel model = new StudentModel();
            model.Id = student.Id;
            model.Name = student.Name.Trim();
            model.LastName = student.LastName.Trim();
            model.Tel = student.Tel.Trim();
            model.Email = student.Email.Trim();
            model.Active = student.Active == 1 ? true : false;
            return View(student);
        }

        public ActionResult Create()
        {
            List<TeacherModel> list = new List<TeacherModel>();
            list = (from t in db.Teachers
                    where t.Active.Equals(1)
                    from s in db.Subjects.Where(s => s.Id.Equals(t.SubjectId)).DefaultIfEmpty()
                    select new TeacherModel
                    {
                        Id = t.Id,
                        Name = t.Name.Trim(),
                        LastName = t.LastName.Trim(),
                        Tel = t.Tel.Trim(),
                        Email = t.Email.Trim(),
                        SubjectId = s.Id,
                        Subject = s.Name,
                        Active = t.Active == 1 ? true : false,
                        SubjectTeacher = s.Name + " - " + t.Name + " " + t.LastName
                    }).ToList();
            ViewBag.SubjectId = new SelectList(list, "Id", "SubjectTeacher");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NumControl,Name,LastName,Tel,Email,MaxSubjects,Active")] StudentModel studentModel)
        {
            if (ModelState.IsValid)
            {
                Student student = new Student();
                student.Id = studentModel.Id;
                student.Name = studentModel.Name;
                student.LastName = studentModel.LastName;
                student.Tel = studentModel.Tel;
                student.Email = studentModel.Email;
                student.Active = 1;
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            StudentModel model = new StudentModel();
            model.Id = student.Id;
            model.Name = student.Name.Trim();
            model.LastName = student.LastName.Trim();
            model.Tel = student.Tel.Trim();
            model.Email = student.Email.Trim();
            model.Active = student.Active == 1 ? true : false;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NumControl,Name,LastName,Tel,Email,MaxSubjects,Active")] StudentModel studentModel)
        {
            if (ModelState.IsValid)
            {
                Student student = new Student();
                student.Id = studentModel.Id;
                student.Name = studentModel.Name;
                student.LastName = studentModel.LastName;
                student.Tel = studentModel.Tel;
                student.Email = studentModel.Email;
                student.Active = 1;
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
