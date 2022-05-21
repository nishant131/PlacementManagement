using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlacementManagement.Models
{
    public class Student
    {
        [Key]
        public int studentId { get; set; }
        [StringLength(50, ErrorMessage = "Student name cannot be longer than 50 characters.")]
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [Range(20,24)]
        public int Age { get; set; }
        [Required]
        [Range(5,10)]
        public float CPI { get; set; }
        [Required]
        [RegularExpression(@"^([6789]\d{9})$", ErrorMessage = "Invalid Mobile Number.")]
        public string MobNumber { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
        public string otherdetails { get; set; }
        [ForeignKey("Dept")]
        public int deptId { get; set; }
        public virtual Department Dept { get; set; }
        public virtual ICollection<Placementstatus> pstatus { get; set; }
    }
}