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

namespace TastyMVC
{
    [Authorize]
    public class RecipesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUser currentUser;
        public RecipesController()
        {
            currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }
        // GET: Recipes
        public ActionResult Index()
        {
            return View(db.Recipes.Include(r => r.Thumbnail).Where(x => x.CreatedBy.ToString() == currentUser.Id).ToList());
        }

        public ActionResult Publish(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe.CreatedBy ==currentUser.Id)
            {
                recipe.Published = !recipe.Published;
                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();
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
            Recipe recipe = db.Recipes
                .Where(x => x.Id == id)
                .Include(r => r.Ingredients.Select(x => x.Ingredient))
                .Include(r => r.Ingredients.Select(x => x.Unit))
                .Include(r => r.Steps.Select(x => x.Image))
                .Include(r => r.Thumbnail).FirstOrDefault();
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
            var allIngredients = db.Ingredients.GroupBy(x => x.Name).Select(x => x.FirstOrDefault()).ToList();
            var allMeasurments = db.MeasurementUnits.ToList();
            return View(new Recipe());
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                recipe.Id = Guid.NewGuid();
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                recipe.CreatedBy = user.Id;

                if (Request.Files.Count > 0)
                {
                    recipe.Thumbnail = new Image(Request.Files[0]);
                }

                db.Recipes.Add(recipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recipe);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIngredient(AddIngredientViewModel addIngredientViewModel)
        {
            if (ModelState.IsValid)
            {
                var ingredient = db.Ingredients.Where(x => x.Name == addIngredientViewModel.Ingredient).FirstOrDefault();
                if (ingredient == null)
                {
                    ingredient = new Ingredient() { Name = addIngredientViewModel.Ingredient };
                    db.Ingredients.Add(ingredient);
                    db.SaveChanges();
                }

                var ingredientSize = new IngredientSize()
                {
                    Amount = addIngredientViewModel.Amount,
                    IngredientID = ingredient.Id,
                    UnitID = addIngredientViewModel.Unit
                };

                var recipe = db.Recipes.Where(x => x.Id == addIngredientViewModel.RecipeID).Include(x => x.Ingredients).FirstOrDefault();

                recipe.Ingredients.Add(ingredientSize);

                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();

            }

            return RedirectToAction("Details", new { id = addIngredientViewModel.RecipeID });
        }
        public ActionResult DeleteIngredient(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var foodIngredient = db.IngredientSizes.Find(id);
            if (foodIngredient == null)
            {
                return HttpNotFound();
            }
            var recipeId = foodIngredient.RecipeId;
            db.IngredientSizes.Remove(foodIngredient);
            db.SaveChanges();

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
                    RecipeID = addFoodStepViewModel.RecipeID
                };

                if (Request.Files.Count > 0)
                {
                    ingredientSize.Image = new Image(Request.Files[0]);
                }
                var recipe = db.Recipes.Where(x => x.Id == addFoodStepViewModel.RecipeID).Include(x => x.Ingredients).FirstOrDefault();

                recipe.Steps.Add(ingredientSize);

                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();

            }

            return RedirectToAction("Details", new { id = addFoodStepViewModel.RecipeID });
        }
        public ActionResult DeleteStep(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var recipeStep = db.RecipeSteps.Find(id);
            if (recipeStep == null)
            {
                return HttpNotFound();
            }
            var recipeId = recipeStep.RecipeID;
            db.RecipeSteps.Remove(recipeStep);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = recipeId });
        }

        // GET: Recipes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,portion")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
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
            Recipe recipe = db.Recipes.Find(id);
            db.Recipes.Remove(recipe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
