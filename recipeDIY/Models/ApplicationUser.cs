﻿using Microsoft.AspNetCore.Identity;

namespace recipeDIY.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        
        [PersonalData]
        public string FirstName { get; set; }
        
        [PersonalData]
        public string LastName { get; set; }
        
        public bool IsPremium { get; set; }
        
    }
}
