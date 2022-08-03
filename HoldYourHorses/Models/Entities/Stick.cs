﻿using System;
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
        public int MaterialId { get; set; }
        public int KategoriId { get; set; }
        public string Beskrivning { get; set; } = null!;
        public int TillverkningslandId { get; set; }
        public bool AbsBroms { get; set; }

        public virtual Kategorier Kategori { get; set; } = null!;
        public virtual Material Material { get; set; } = null!;
        public virtual Tillverkningsländer Tillverkningsland { get; set; } = null!;
    }
}
