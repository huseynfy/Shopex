using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public SliderText SliderTexts { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Product> ProductsMini { get; set; }
        public Contact Contacts { get; set; }
        public PromotedProduct PromotedProducts { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public Product Product { get; set; }
        public Image Image{ get; set; }
        public IEnumerable<ProductReview> ProductReviews { get; set; }
        public IEnumerable<Cart> Carts { get; set; }


        [Required, StringLength(50)]
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; }


        public bool RememberMe { get; set; }

        public AppUser AppUser { get; set; }
    }
}
