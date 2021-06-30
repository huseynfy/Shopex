using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        [NotMapped]
        public IFormFile Photo1 { get; set; }
        [NotMapped]
        public IFormFile Photo2 { get; set; }
        [NotMapped]
        public IFormFile Photo3 { get; set; }
        [NotMapped]
        public IFormFile Photo4 { get; set; }
    }
}
