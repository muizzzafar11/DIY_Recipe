using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipeDIY.Data;
using recipeDIY.Models;

namespace recipeDIY.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]/[action]/")]
    public class RecipeController : Controller
    {
        private ApplicationDbContext _db;
        public RecipeController(ApplicationDbContext context)
        {
            this._db = context;
        }

        #nullable enable
        // GET: api/GetRecipe/userId?
        [HttpGet]
        [AllowAnonymous]
        public List<Recipe> GetRecipe(string? userId)
        {
            // if the ID is included, and user is premium, premium content will also be included in the results
            // Otherwise, we will display the users own content (excluding the premium content)
            // if the id is not included, return all recipes except for premium and private ones
            if (userId != "")
            {
                var premiumCheck = _db.Users.FirstOrDefault(u => u.Id.ToString() == userId).IsPremium;
                if (premiumCheck) return _db.Recipes.ToList();
                
                    return _db.Recipes.Include(r => r.Author)
                        .Where(r => r.Id.ToString() == userId)
                        .Where(r => r.IsPremium == false).ToList();
            }
            
            var recipes = _db.Recipes.Where(r => r.IsPrivate == false).Where(r => r.IsPremium == false).ToList();
            Console.WriteLine(recipes);
            
            return recipes;

        }
        
        [HttpDelete]
        public IActionResult DeleteRecipe(int recipeId)
        {
            var recipe = _db.Recipes.Find(recipeId);  
            if (recipe == null)  
            {  
                return NotFound();  
  
            }  
            _db.Recipes.Remove(recipe);  
            _db.SaveChanges(); 
            
            return Ok();
        }
        
        
        public IActionResult GetSingleRecipe(ApplicationUser author, int recipeId)
        {
            // return View();
            return null;
        }

    }
}