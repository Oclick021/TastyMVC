using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TastyMVC.Models;

namespace TastyMVC.ViewModels
{
    public class RecipeViewModel
    {
        public Recipe Recipe { get; set; }
        public List<Ingredient> AllIngredients { get; set; }
    }
}