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
        
        private readonly ApplicationDbContext _db;

        private List<Recipe.RecipeCategory> categoryNames = new List<Recipe.RecipeCategory>()
        {
            Recipe.RecipeCategory.Any, 
            Recipe.RecipeCategory.Breakfast, 
            Recipe.RecipeCategory.Lunch,
            Recipe.RecipeCategory.Dinner
        };

        public MyRecipe(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            
            _db = db;
        }


        [BindProperty] public Recipe Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var recipe = new Recipe()
                {
                    Author = await _userManager.GetUserAsync(User),
                    Content = Input.Content,
                    IsPrivate = Request.Form["private"] == "true",
                    IsPremium = Request.Form["premium"] == "true",
                    PostDate = DateTime.Now,
                    Category = categoryNames[Int32.Parse(Request.Form["dropdown_category"])],
                };

                await _db.Recipes.AddAsync(recipe);
                await _db.SaveChangesAsync();
                return Page();
            }

            return Page();
        }
    }
}