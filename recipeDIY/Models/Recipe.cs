using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipeDIY.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        
        public ApplicationUser Author { get; set; }
        
        public DateTime PostDate { get; set; }
        
        public DateTime EditDate { get; set; }
        
        public string Content { get; set; }

        public RecipeCategory Category { get; set; }
        
        [NotMapped]
        public string CategoryName { get; set; }
        
        public bool IsPrivate { get; set; }
        
        public bool IsPremium { get; set; }
        
        public bool IsDeleted { get; set; }

        public enum RecipeCategory
        {
            Any,
            Breakfast,
            Lunch,
            Dinner,
        }
        
        private readonly Dictionary<RecipeCategory, String> DisplayNameMapping = new Dictionary<RecipeCategory, string>()
        {
            { RecipeCategory.Any, "any" },
            { RecipeCategory.Breakfast, "breakfast" },
            { RecipeCategory.Lunch, "lunch" },
            { RecipeCategory.Dinner, "dinner" }
        };

        public Recipe()
        {
        }

        public Recipe(RecipeCategory category)
        {
            this.Category = category;
            this.CategoryName = this.DisplayNameMapping[category];
        }
    }
}