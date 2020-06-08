using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace App.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string CustomTag { get; set; }
        public IEnumerable<UserVote> VotedFeatures { get; set; }
    }
}