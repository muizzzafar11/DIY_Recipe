using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using recipeDIY.Areas.Identity.Pages.Account;
using recipeDIY.Data;
using recipeDIY.Models;

namespace recipeDIY.Areas.Identity.Pages
{
    public class AllRecipe : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        
        public List<Recipe> AllRecipes = new List<Recipe>();
        public AllRecipe(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public void OnGet()
        {
            AllRecipes = _AllRecipeList();
        }
        
        private List<Recipe> _AllRecipeList()
        {
            return _db.Recipes
                .Where(r => r.IsPremium == false).Where(r => r.IsPrivate == false)
                .OrderBy(r => r.PostDate).ToList();
        }
    }
}