using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestHighSchool.Models
{
    public class TeacherModel
    {
        public int Id { get; set; }

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
        public int SubjectId { get; set; }

        [Display(Name = "Materia")]
        public string Subject { get; set; }

        [Display(Name = "MateriaMaestro")]
        public string SubjectTeacher { get; set; }

        public bool Active { get; set; }
    }
}