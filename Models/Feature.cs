using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Feature
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Created At")]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Supporters Count")]
        public int VoteCount { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Due Date")]
        [DisplayFormat(DataFormatString = "{0:d MMMM yyyy}")]
        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}