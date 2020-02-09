using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TastyMVC.Data;
using TastyMVC.Models;

namespace TastyMVC.ViewModels
{
    public class AddIngredientViewModel
    {
        public Guid IngredientRecipeID { get; set; }
        [Required]
        [Display(Name ="Ingredient name please")]
        public string Ingredient { get; set; }
        public Guid Unit { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public IEnumerable<Ingredient> AllIngredients { get; set; }
        public IEnumerable<MeasurementUnit> AllUnits { get; set; }
        public AddIngredientViewModel(Guid recipeID)
        {
            IngredientRecipeID = recipeID;
            var rep = new Repository();
            AllIngredients = rep.GetAllIngredients();
            AllUnits = rep.GetAllUnits();
        }
        public AddIngredientViewModel()
        {

        }
    }
}