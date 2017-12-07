using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TradeCenter.Models
{
    public class Product
    {
        public const int PICTURES_COUNT = 3;

        public long ID { get; set; }

        [ForeignKey("Owner")]
        public long OwnerID { get; set; }

        public long? UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string ShortDescription { get; set; }

        [Required]
        [StringLength(4000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Details")]
        public string LongDescription { get; set; }

        [Display(Name = "Published")]
        [DataType(DataType.Date)]
        public DateTime DatePublished { get; set; }

        [Display(Name = "Edited")]
        [DataType(DataType.Date)]
        public DateTime? DateEdited { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C}" /*, ApplyFormatInEditMode = true*/)]
        public decimal Price { get; set; }

        [Display(Name = "Picture 1")]
        public byte[] Picture1 { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Picture1_MimeType { get; set; }

        [Display(Name = "Picture 2")]
        public byte[] Picture2 { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Picture2_MimeType { get; set; }

        [Display(Name = "Picture 3")]
        public byte[] Picture3 { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Picture3_MimeType { get; set; }

        public ProductState State { get; set; }

        public User Owner { get; set; }

        [NotMapped]
        [Display(Name = "Owner")]
        public string OwnerName { get { return Owner?.ToString(); } }
    }
}