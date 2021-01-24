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

        // GET: api/GetAllRecipe/userId?
        [HttpGet]
        public List<Recipe> GetAllRecipe(int? userId)
        {
            // if the ID is included, and user is premium, premium content will also be included in the results
            // Otherwise, we will display the users own content (excluding the premium content)
            var recipes = _db.Recipes.Include(r => r.Author).ToList();

            return recipes;
        }
        
        [HttpPut("{recipeId}")]
        public IActionResult EditRecipe(int recipeId)
        {
            // return View();
            return null;
        }
        
        [HttpDelete]
        public IActionResult DeleteRecipe(int recipeId)
        {
            // return View();
            return null;
        }
        
        
        public IActionResult GetSingleRecipe(ApplicationUser author, int recipeId)
        {
            // return View();
            return null;
        }
        
        


        
    }
}