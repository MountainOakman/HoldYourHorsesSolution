﻿namespace HoldYourHorses.Views.Sticks
{
    public class DetailsVM
    {
        public int Artikelnr { get; set; }
        public decimal Pris { get; set; }
        public int Hästkrafter { get; set; }
        public int Trädensitet { get; set; }
        public string Artikelnamn { get; set; } 
        public string Material { get; set; } 
        public string Typ { get; set; } 
        public string Beskrivning { get; set; } 
        public string Tillverkningsland { get; set; } 
        public bool AbsBroms { get; set; }
        public string Bild { get; set; }

    }
}