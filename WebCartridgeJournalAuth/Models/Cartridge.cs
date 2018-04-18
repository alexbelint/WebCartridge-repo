using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCartridgeJournalAuth.Models
{
    public class Cartridge
    {
        public int CartridgeID { get; set; }
        public int ColorID { get; set; }
        public virtual Color Color { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата покупки")]
        public DateTime? Purchase_Date { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата установки")]
        public DateTime? Installation_Date { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата списания")]
        public DateTime? Deinstallation_Date { get; set; }
    }
}