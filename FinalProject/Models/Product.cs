using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Detail { get; set; }
        public string Description { get; set; }
        public int Sale { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int ImageId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Image Image { get; set; }
    }
}
