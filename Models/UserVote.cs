using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace App.Models
{
    public class UserVote
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        
        public int FeatureId { get; set; }
        public Feature Feature { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
    }
}