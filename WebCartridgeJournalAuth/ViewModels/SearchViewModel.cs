using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCartridgeJournalAuth.Models;

namespace WebCartridgeJournalAuth.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<Cartridge> Catridges { get; set; }
        public Cartridge SearchedCartridge { get; set; }
        public Cartridge SearchedCartridgeForInput2 { get; set; }
        //public int CartridgeCount { get; set; }
    }
}