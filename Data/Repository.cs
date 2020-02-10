using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TastyMVC.Models;

namespace TastyMVC.Data
{
    public class Repository
    {
        private ApplicationDbContext dbContext;
        public Repository()
        {
            dbContext = new ApplicationDbContext();
        }

        public IEnumerable<Ingredient> GetAllIngredients()
        {
            var allIngredients = dbContext.Ingredients.GroupBy(x => x.Name).Select(x => x.FirstOrDefault());
            return allIngredients;
        }
        public Ingredient GetIngredientByName(string name)
        {
            return dbContext.Ingredients.Where(x => x.Name == name).FirstOrDefault();
        }
        public IEnumerable<MeasurementUnit> GetAllUnits()
        {
            return dbContext.MeasurementUnits;
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return dbContext.Categories;
        }
        public Dictionary<Category, int> GetCategoriesWithCount()
        {
            var dictionary = new Dictionary<Category, int>();
            var categories = GetAllCategories();
            var allrecipes = GetPublishedRecipes();

            foreach (var category in categories)
            {
                var count = allrecipes.Where(x => x.Category.Id == category.Id).Count();
                dictionary.Add(category, count);
            }
            return dictionary;
        }
        public Category GetCategory(Guid Id)
        {
            return dbContext.Categories.Find(Id);
        }

        public Recipe GetRecipe(Guid id)
        {
            return dbContext.Recipes
                  .Where(x => x.Id == id)
                  .Include(r => r.Ingredients.Select(x => x.Ingredient))
                  .Include(r => r.Ingredients.Select(x => x.Unit))
                  .Include(r => r.Steps.Select(x => x.Image))
                  .Include(r => r.Thumbnail)
                  .Include(x => x.Category).FirstOrDefault();
        }
        public void UpdateRecipe(Recipe recipe)
        {
            dbContext.Entry(recipe).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public IEnumerable<Recipe> GetUsersRecipes(string userID)
        {
            return dbContext.Recipes.Include(r => r.Thumbnail).Where(x => x.CreatedBy.ToString() == userID).ToList();
        }
        public IEnumerable<Recipe> GetPublishedRecipesByCategoryName(string categoryName)
        {
            var recipes = GetPublishedRecipes();
            return recipes.Where(x => x.Category.Name == categoryName);
        }


        public IEnumerable<Recipe> GetPublishedRecipes()
        {
            return dbContext.Recipes
                      .Where(x => x.Published == true)
                      .Include(r => r.Ingredients.Select(x => x.Ingredient))
                      .Include(r => r.Ingredients.Select(x => x.Unit))
                      .Include(r => r.Steps.Select(x => x.Image))
                      .Include(r => r.Thumbnail)
                      .Include(x => x.Category).ToList();
        }
        public IEnumerable<Recipe> SearchPublishedRecipes(string search)
        {
            var recipes = dbContext.Recipes
                      .Where(x => x.Published == true )
                      .Include(r => r.Ingredients.Select(x => x.Ingredient))
                      .Include(r => r.Ingredients.Select(x => x.Unit))
                      .Include(r => r.Steps.Select(x => x.Image))
                      .Include(r => r.Thumbnail)
                      .Include(x => x.Category).ToList();
            var found = new List<Recipe>();
            foreach (var recipe in recipes)
            {
                if (recipe.Title.Contains(search) ||
                    recipe.Ingredients.Any(i => i.Ingredient.Name.Contains(search)) ||
                    recipe.Steps.Any(s => s.Title.Contains(search) || s.Description.Contains(search)))
                {
                    found.Add(recipe);
                }
            }
            return found;

        }

    }
}