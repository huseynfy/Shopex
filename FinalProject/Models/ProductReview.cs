using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class ProductReview
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Commentator { get; set; }
        public DateTime DateTime { get; set; }
        public string Comment { get; set; }
    }
}
