using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class PromotedProduct
    {
        public int Id { get; set; }
        public string ImageBig { get; set; }
        public string ImageSmall1 { get; set; }
        public string ImageSmall2 { get; set; }
        public string ImageMedium { get; set; }

        [NotMapped]
        public IFormFile PhotoBig { get; set; }
        [NotMapped]
        public IFormFile PhotoSmall1 { get; set; }
        [NotMapped]
        public IFormFile PhotoSmall2 { get; set; }
        [NotMapped]
        public IFormFile PhotoMedium { get; set; }
    }
}
