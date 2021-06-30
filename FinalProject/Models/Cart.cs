using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProId { get; set; }
        public string ProName { get; set; }
        public double ProPrice { get; set; }
        public int ProCount { get; set; }
        public string Username { get; set; }
        public string ProImage { get; set; }
        public double Total { get; set; }
    }
}
