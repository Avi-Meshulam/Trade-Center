using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;

namespace TradeCenter.Models
{
    public class User
    {
        public User()
        {
            Products = new HashSet<Product>();
        }

        public long ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        // It seems like DisplayFormat is ignored for TextBoxes, 
        // so format is set instead in Views/Shared/EditorTemplates/Date.cshtml
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [Index(IsUnique = true)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        // Collection of products, currently owned by user
        public virtual ICollection<Product> Products { get; set; }
    }
}