using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashQuizz_Nima_Zarrabi.Models
{
    public class Card
    {
        public int Id {  get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public string? Hint { get; set; }

        public override string ToString()
        {
            return $"[Card {Id}]";
        }
    }
}
