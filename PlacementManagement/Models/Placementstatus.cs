using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlacementManagement.Models
{
    public class Placementstatus
    {
        [Key]
        [Column(Order = 1)]
        public int companyId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int studentId { get; set; }
        public virtual Company company { get; set; }
        public virtual Student student { get; set; }
        [Required]
        public string Status { get; set; }
    }
}