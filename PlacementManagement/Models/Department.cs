using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlacementManagement.Models
{
    public class Department
    {
        [Key]
        public int deptId { get; set; }
        [StringLength(50, ErrorMessage = "Department name cannot be longer than 50 characters.")]
        [Required]
        public string deptName { get; set; }
        public virtual Admin admin { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}