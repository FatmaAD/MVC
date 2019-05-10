using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mvc_project_auth.Models.Entities
{
    [Table("Employee")]
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int Age { get; set; }

        [Required]
        public int Salary { get; set; }

        [Required]
        public Enums.Gender Gender { get; set; }

        [ForeignKey("Department")]
        [Required(ErrorMessage = "Please select a Dept")]
        public int Fk_DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}