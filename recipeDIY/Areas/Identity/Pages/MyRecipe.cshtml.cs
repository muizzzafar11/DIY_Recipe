using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using recipeDIY.Data;
using recipeDIY.Models;

namespace recipeDIY.Areas.Identity.Pages
{
    public class MyRecipe : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<InputModel> _logger;
        private readonly ApplicationDbContext _db;
        
        public MyRecipe(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<InputModel> logger, 
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _db = db;
        }



        [BindProperty] public InputModel Input { get; set; }

        public class InputModel
        {
            // [Required] 
            [Display(Name = "Author")]
            public ApplicationUser Author { get; set; }
            
            // [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Content")]
            public string Content { get; set; }

            // [Required]
            [Display(Name = "IsPrivate")]
            public bool IsPrivate { get; set; }

            // [Required]
            [Display(Name = "IsPremium")]
            public bool IsPremium { get; set; }

            
            [Display(Name = "IsDeleted")]
            public bool IsDeleted { get; set; }

            // [Required]
            [Display(Name = "Category")]
            public Recipe.RecipeCategory Category { get; set; }

            public string CategoryName { get; set; }

            // [Required]
            [DataType(DataType.DateTime)]
            [Display(Name = "PostDate")]
            public DateTime PostDate { get; set; }

            [DataType(DataType.DateTime)]
            [Display(Name = "EditDate")]
            public DateTime EditDate { get; set; }
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                
                
                var recipe = new Recipe(Input.Category)
                {
                    Author = await _userManager.GetUserAsync(User), 
                    Content = "Input.Content",
                    IsPrivate = false,
                    IsPremium = false,
                    // PostDate = Input.PostDate,
                    Category = Recipe.RecipeCategory.Breakfast,
                };

                await _db.Recipes.AddAsync(recipe);
                await _db.SaveChangesAsync();
                return Page();
            }
        
            return Page();
        }
    }
}