using System;
using System.ComponentModel.DataAnnotations;

namespace Sprout.Exam.Common.Models
{

    public partial class Employee
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        public string TIN { get; set; }
        [Required]
        [StringLength(100)]
        public int EmployeeTypeId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
