using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlacementManagement.Models
{
    public class Company
    {
        [Key]
        public int companyId { get; set; }
        [StringLength(50, ErrorMessage = "Company name cannot be longer than 50 characters.")]
        [Required]
        public string companyName { get; set; }
        [Required]
        public string location { get; set; }
        [Required]
        [Range(5, 10)]
        public float minRequirements { get; set; }
        [Required]
        public decimal avgPackage { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime arrivalDate { get; set; }
        public virtual ICollection<Placementstatus> pstatus { get; set; }
    }
}