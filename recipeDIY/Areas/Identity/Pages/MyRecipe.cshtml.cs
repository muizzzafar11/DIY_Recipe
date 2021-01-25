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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public List<Recipe> AllUserRecipes = new List<Recipe>();
        public bool editMode = false;

        private readonly List<Recipe.RecipeCategory> _categoryNames = new List<Recipe.RecipeCategory>()
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


        [BindProperty] 
        public Recipe Input { get; set; }

        public void OnGet()
        {
            AllUserRecipes = _currentUserRecipeList();
        }


        private List<Recipe> _currentUserRecipeList()
        {
            return _db.Recipes.Include(r => r.Author)
                .Where(r => r.Author.Email == User.Identity.Name)
                .OrderBy(r => r.PostDate).ToList();
        }

        public async Task<IActionResult> OnPostRemoveAsync(int id)  
        {  
            var recipe = await _db.Recipes.FindAsync(id);  
            if (recipe == null)  
            {  
                return NotFound();  
  
            }  
            _db.Recipes.Remove(recipe);  
            await _db.SaveChangesAsync();  
  
            AllUserRecipes = _currentUserRecipeList();
            return RedirectToPage("MyRecipe");
        }  
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                int number = 0;
                var formData = Request.Form["dropdown_category"];
                if (formData != "") Int32.Parse(formData);
                var recipe = new Recipe()
                {
                    Author = await _userManager.GetUserAsync(User),
                    RecipeName = Input.RecipeName,
                    Content = Input.Content,
                    IsPrivate = Request.Form["private"] == "true",
                    IsPremium = Request.Form["premium"] == "true",
                    PostDate = DateTime.Now,
                    Category = _categoryNames[number],
                };

                if(recipe.RecipeName != null && recipe.Content != null) 
                {
                    await _db.Recipes.AddAsync(recipe);
                    await _db.SaveChangesAsync();
                }
            }
            AllUserRecipes = _currentUserRecipeList();
            return Page();
        }
        
        public ActionResult Edit(int Id)
        { 
            //here, get the student from the database in the real application
            
            //getting a student from collection for demo purpose
            var std = _db.Recipes.Where(s => s.Id == Id).FirstOrDefault();
    
            return View(std);
        }
    }
}