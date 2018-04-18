using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCartridgeJournalAuth.Models
{
    public class Brand
    {
        public int BrandID { get; set; }
        [Display(Name = "Модель")]
        public string BrandName { get; set; }
        public virtual ICollection<Cartridge> Cartridges { get; set; }
    }
}