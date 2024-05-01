using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support_AppMobile.Models
{
    public class GreaterWish
    {
        public int Id {  get; set; }

        /* Définition du voeu, par exemple 
        "marcher sur la lune" */
        public string? Definition { get; set; }

        /* Date à laquelle le voeu a été exaucé */
        public DateTime AccomplishedDate { get; set; }

        public override string ToString()
        {
            return $"[GreaterWish {Id}]";
        }
    }
}
