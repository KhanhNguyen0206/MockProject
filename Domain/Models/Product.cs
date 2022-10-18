using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using FluentValidation;

namespace Domain.Models
{
    public class Product
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Product Description")]
        public string Description { get; set; }

        [Required]
        [ValidateNever]
        public double Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime InsAt { get; set; } = DateTime.Now;

    }

    
}
