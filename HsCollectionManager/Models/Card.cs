using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HsCollectionManager.Models
{
    public class Card
    {
        public string Name { get; set; }
        public int ManaCost { get; set; }
        public string Category { get; set; }
        public string Rarity { get; set; }
        public string Img { get; set; }
        public int Id { get; set; }
    }
}