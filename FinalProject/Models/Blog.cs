using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        [Required]
        public string Commentator { get; set; }
        [Required]
        public int Comments { get; set; }
        [Required,StringLength(410)]
        public string Description { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
