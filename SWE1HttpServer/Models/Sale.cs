using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Sale
    {
        public string Id { get; set; }
        public string CardToSell { get; set; }
        public int Coins { get; set; }
    }
}
