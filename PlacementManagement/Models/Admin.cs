using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlacementManagement.Models
{
    public class Admin
    {
        [Key]
        [ForeignKey("dept")]
        public int adminId { get; set; }
        [StringLength(50, ErrorMessage = "Admin name cannot be longer than 50 characters.")]
        [Required]
        public string adminName { get; set; }
        [Required]
        public string Password { get; set; }
        public virtual Department dept { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
    }
}