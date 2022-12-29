using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestHighSchool.Models
{
    public class StudentModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "NumControl")]
        public int NumControl { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Nombres")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Telefono")]
        public string Tel { get; set; }

        [Required]
        [MaxLength(45)]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [Required]
        public int Subject1Id { get; set; }

        [Required]
        [Display(Name = "Materia1")]
        public string Subject1 { get; set; }

        [Required]
        public int Subject2Id { get; set; }

        [Required]
        [Display(Name = "Materia2")]
        public string Subject2 { get; set; }

        [Required]
        public int Subject3Id { get; set; }

        [Required]
        [Display(Name = "Materia3")]
        public string Subject3 { get; set; }

        [Required]
        public int Subject4Id { get; set; }

        [Required]
        [Display(Name = "Materia4")]
        public string Subject4 { get; set; }

        [Required]
        public int Subject5Id { get; set; }

        [Required]
        [Display(Name = "Materia5")]
        public string Subject5 { get; set; }

        //[Required]
        //[Display(Name = "Maximo de Materias")]
        //public int MaxSubjects { get; set; }

        public bool Active { get; set; }
    }
}