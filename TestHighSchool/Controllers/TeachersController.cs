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

namespace TestHighSchool.Content
{
    public class TeachersController : Controller
    {
        private DB_TestHighSchoolEntities db = new DB_TestHighSchoolEntities();

        public ActionResult Index()
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
                    }).ToList();
            return View(list);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            TeacherModel model = new TeacherModel();
            model.Id = teacher.Id;
            model.Name = teacher.Name.Trim();
            model.LastName = teacher.LastName.Trim();
            model.Tel = teacher.Tel.Trim();
            model.Email = teacher.Email.Trim();
            model.SubjectId = teacher.SubjectId;
            model.Active = teacher.Active == 1 ? true : false;
            return View(teacher);
        }

        public ActionResult Create()
        {
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LastName,Tel,Email,SubjectId,Active")] TeacherModel teacherModel)
        {
            if (ModelState.IsValid)
            {
                Teacher teacher = new Teacher();
                teacher.Id = teacherModel.Id;
                teacher.Name = teacherModel.Name;
                teacher.LastName = teacherModel.LastName;
                teacher.Tel = teacherModel.Tel;
                teacher.Email = teacherModel.Email;
                teacher.SubjectId = teacherModel.SubjectId;
                teacher.Active = 1;
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", teacherModel.SubjectId);
            return View(teacherModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            TeacherModel model = new TeacherModel();
            model.Id = teacher.Id;
            model.Name = teacher.Name.Trim();
            model.LastName = teacher.LastName.Trim();
            model.Tel = teacher.Tel.Trim();
            model.Email = teacher.Email.Trim();
            model.SubjectId = teacher.SubjectId;
            model.Active = teacher.Active == 1 ? true : false;
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", model.SubjectId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LastName,Tel,Email,SubjectId,Active")] TeacherModel teacherModel)
        {
            if (ModelState.IsValid)
            {
                Teacher teacher = new Teacher();
                teacher.Id = teacherModel.Id;
                teacher.Name = teacherModel.Name;
                teacher.LastName = teacherModel.LastName;
                teacher.Tel = teacherModel.Tel;
                teacher.Email = teacherModel.Email;
                teacher.SubjectId = teacherModel.SubjectId;
                teacher.Active = 1;
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", teacherModel.SubjectId);
            return View(teacherModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
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
