using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCartridgeJournalAuth.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        [Display(Name = "Отдел")]
        public string DepatmentName { get; set; }
        public virtual ICollection<Cartridge> Cartridges { get; set; }
    }
}