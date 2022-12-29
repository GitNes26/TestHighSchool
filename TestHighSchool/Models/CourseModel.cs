using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestHighSchool.Models
{
    public class CourseModel
    {
        public int Id { get; set; }

        [Required]
        public int TeacherId { get; set; }

        //[Required]
        [Display(Name = "Profesor")]
        public string Teacher { get; set; }

        [Required]
        public int StudentId { get; set; }

        //[Required]
        [Display(Name = "Alumno")]
        public string Student { get; set; }

        [Display(Name = "Materia")]
        public string Subject { get; set; }

        //[Required]
        [Display(Name = "Evaluacion 1")]
        public decimal Evaluation1 { get; set; }

        [Display(Name = "Evaluacion 2")]
        public decimal Evaluation2 { get; set; }

        [Display(Name = "Evaluacion 3")]
        public decimal Evaluation3 { get; set; }

        [Display(Name = "Promedio Final")]
        public decimal FinalAverage { get; set; }
    }
}