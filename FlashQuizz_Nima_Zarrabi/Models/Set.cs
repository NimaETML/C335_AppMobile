using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashQuizz_Nima_Zarrabi.Models
{
    public class Set
    {
        public int Id {  get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreationDate { get; set; }

        public override string ToString()
        {
            return $"[Set {Id}]";
        }
    }
}
