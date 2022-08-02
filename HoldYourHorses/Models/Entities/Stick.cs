using System;
using System.Collections.Generic;

namespace HoldYourHorses.Models.Entities
{
    public partial class Stick
    {
        public int Artikelnr { get; set; }
        public decimal Pris { get; set; }
        public int Hästkrafter { get; set; }
        public int Trädensitet { get; set; }
        public string Artikelnamn { get; set; } = null!;
        public string Material { get; set; } = null!;
        public string Typ { get; set; } = null!;
        public string Beskrivning { get; set; } = null!;
        public string Tillverkningsland { get; set; } = null!;
        public bool AbsBroms { get; set; }
    }
}
