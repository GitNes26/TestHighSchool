using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestHighSchool.Models
{
    public class SubjectModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [MaxLength(100)]
        public string Name { get; set; }

        public bool Active { get; set; }
    }
}