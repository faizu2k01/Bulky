﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkyRazorWebApp.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Category Name")]
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display order value should be in between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
