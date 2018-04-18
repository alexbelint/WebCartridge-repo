using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCartridgeJournalAuth.Models
{
    public class Color
    {
        public int ColorId { get; set; }
        [Display(Name = "Цвет")]
        public string ColorName { get; set; }
        public virtual ICollection<Cartridge> Cartridges { get; set; }
    }
}