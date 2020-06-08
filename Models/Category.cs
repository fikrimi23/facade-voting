using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        public List<Feature> Features { get; set; }
    }
}