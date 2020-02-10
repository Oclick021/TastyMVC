using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TastyMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using TastyMVC.ViewModels;
using TastyMVC.Data;

namespace TastyMVC
{
    [Authorize]
    public class RecipesController : Controller
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();

        private Repository repository;
        private ApplicationUser currentUser;
        public RecipesController()
        {
            repository = new Repository();
            currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }
        // GET: Recipes
        public ActionResult Index()
        {
            
            return View(repository.GetUsersRecipes(currentUser.Id));
        }

        public ActionResult Publish(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = repository.GetRecipe(id.Value);
            if (recipe.CreatedBy == currentUser.Id)
            {
                recipe.Published = !recipe.Published;
                dbContext.Entry(recipe).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

        }


        // GET: Recipes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = repository.GetRecipe(id.Value);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            if (recipe.CreatedBy == currentUser.Id)
            {
                ViewBag.IsOwner = true;
            }
            else
            {
                ViewBag.IsOwner = false;
            }
            return View(recipe);
        }

        // GET: Recipes/Create
        public ActionResult Create()
        {
            var repo = new Repository();
            return View("RecipeForm", new RecipeViewModel(repo.GetAllCategories()));
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrUpdate(RecipeViewModel recipeViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                Recipe recipe = null;
                if (Guid.TryParse(recipeViewModel.RecipeID, out Guid recipeId))
                {
                    recipe = repository.GetRecipe(recipeId);
                }
                if (recipe == null)// new Recipe
                {
                    recipe = new Recipe()
                    {
                        Title = recipeViewModel.Title,
                        CreatedBy = user.Id,
                        Id = Guid.NewGuid()
                    };
                    if (Guid.TryParse(recipeViewModel.Category.ToString(), out Guid catId))
                    {
                        var category = repository.GetCategory(catId);
                        recipe.Category = category;
                    }
                    if (Request.Files.Count > 0)
                    {
                        recipe.Thumbnail = new Image(Request.Files[0]);
                    }
                    dbContext.Recipes.Add(recipe);
                    dbContext.SaveChanges();
                    TempData["Message"] = "You can now add details like steps and ingredients to your recipe";
                }
                else //update
                {
                    recipe.Title = recipeViewModel.Title;
                    if (Request.Files.Count > 0)
                    {
                        recipe.Thumbnail = new Image(Request.Files[0]);
                    }
                    recipe.Portion = recipeViewModel.portion;
                    if (Guid.TryParse(recipeViewModel.Category.ToString(), out Guid catId))
                    {
                        var category = repository.GetCategory(catId);
                        recipe.Category = category;
                    }
                    repository.UpdateRecipe(recipe);
                    TempData["Message"] = "Recipe Updated!";
                }

                return RedirectToAction("Details", new { id = recipe.Id });

            }

            return View(recipeViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIngredient(AddIngredientViewModel addIngredientViewModel)
        {
            if (ModelState.IsValid)
            {
                var ingredient = repository.GetIngredientByName(addIngredientViewModel.Ingredient);
                if (ingredient == null)
                {
                    ingredient = new Ingredient() { Name = addIngredientViewModel.Ingredient };
                    dbContext.Ingredients.Add(ingredient);
                    dbContext.SaveChanges();
                }

                var ingredientSize = new IngredientSize()
                {
                    Amount = addIngredientViewModel.Amount,
                    IngredientID = ingredient.Id,
                    UnitID = addIngredientViewModel.Unit
                };

                var recipe = repository.GetRecipe(addIngredientViewModel.IngredientRecipeID);

                recipe.Ingredients.Add(ingredientSize);

                repository.UpdateRecipe(recipe);

                TempData["Message"] = $"{addIngredientViewModel.Ingredient} added to {recipe.Title}";

            }

            return RedirectToAction("Details", new { id = addIngredientViewModel.IngredientRecipeID });
        }
        public ActionResult DeleteIngredient(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var foodIngredient = dbContext.IngredientSizes.Find(id);
            if (foodIngredient == null)
            {
                return HttpNotFound();
            }
            var recipeId = foodIngredient.RecipeId;
            dbContext.IngredientSizes.Remove(foodIngredient);
            dbContext.SaveChanges();
            TempData["Message"] = $"Ingredient removed!";

            return RedirectToAction("Details", new { id = recipeId });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStep(AddFoodStepViewModel addFoodStepViewModel)
        {
            if (ModelState.IsValid)
            {

                var ingredientSize = new RecipeStep()
                {
                    Description = addFoodStepViewModel.Description,
                    Title = addFoodStepViewModel.Title,
                    Order = addFoodStepViewModel.Order,
                    RecipeID = addFoodStepViewModel.StebRecipeID
                };

                if (Request.Files.Count > 0)
                {
                    ingredientSize.Image = new Image(Request.Files[0]);
                }
                var recipe = repository.GetRecipe(addFoodStepViewModel.StebRecipeID);

                recipe.Steps.Add(ingredientSize);

                repository.UpdateRecipe(recipe);

                TempData["Message"] = $"Step added to {recipe.Title}";
            }

            return RedirectToAction("Details", new { id = addFoodStepViewModel.StebRecipeID });
        }
        public ActionResult DeleteStep(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var recipeStep = dbContext.RecipeSteps.Find(id);
            if (recipeStep == null)
            {
                return HttpNotFound();
            }
            var recipeId = recipeStep.RecipeID;
            dbContext.RecipeSteps.Remove(recipeStep);
            dbContext.SaveChanges();

            return RedirectToAction("Details", new { id = recipeId });
        }

        // GET: Recipes/Edit/5
        public ActionResult Edit(Guid? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rep = new Repository();

            Recipe recipe = repository.GetRecipe(id.Value);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            var recipeViewModel = new RecipeViewModel(rep.GetAllCategories())
            {
                Category = recipe.Category.Id,
                portion = recipe.Portion,
                ThumbNail = recipe.Thumbnail,
                RecipeID = recipe.Id.ToString(),
                Title = recipe.Title
            };
            return View("RecipeForm", recipeViewModel);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Title,portion")] Recipe recipe)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        dbContext.Entry(recipe).State = EntityState.Modified;
        //        dbContext.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(recipe);
        //}

        // GET: Recipes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = dbContext.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Recipe recipe = dbContext.Recipes.Find(id);
            dbContext.Recipes.Remove(recipe);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
