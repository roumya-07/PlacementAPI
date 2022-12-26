using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlacementAPI.Models
{
    public class Student
    {
        [Key]
        public int Sl_No { get; set; } = 0;
        [Required]
        public string Roll_No { get; set; } = null;
        [Required]
        public string Name { get; set; } = null;
        [Required]
        public string DOB { get; set; } = null;
        [Required]
        public int BranchID { get; set; } = 0;
        [Required]
        public string Passing_Year { get; set; } = null;
        
        [Required]
        public decimal CGPA { get; set; } = 0;
        [Required]
        public string BackLog { get; set; } = null;

        [NotMapped]
        public string BranchName { get; set; } = null;   
    }
    public class Branch
    {
        [Key]
        public int BranchID { get; set; } = 0;
        [Required]
        public string BranchName { get; set; } = null;
    }
}
