using System;
using System.Collections.Generic;

namespace HoldYourHorses.Models.Entities
{
    public partial class Ordrar
    {
        public Ordrar()
        {
            Orderraders = new HashSet<Orderrader>();
        }

        public int Id { get; set; }
        public string Förnamn { get; set; } = null!;
        public string Efternamn { get; set; } = null!;
        public string Epost { get; set; } = null!;
        public string Stad { get; set; } = null!;
        public int Postnummer { get; set; }
        public string Adress { get; set; } = null!;
        public string Land { get; set; } = null!;

        public virtual ICollection<Orderrader> Orderraders { get; set; }
    }
}
