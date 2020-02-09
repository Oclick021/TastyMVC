using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TastyMVC.Models;

namespace TastyMVC.ViewModels
{
    public class RecipeViewModel
    {
        
        public string RecipeID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int portion { get; set; }
        [Required]
        public Guid Category { get; set; }
        public Image ThumbNail { get; set; }

        public IEnumerable<Category> AllCategories { get; set; }
        public RecipeViewModel()
        {

        }
        public RecipeViewModel(IEnumerable<Category> categories,string recipeID = null, Image thumbNail = null)
        {
            AllCategories = categories;
            RecipeID = recipeID;
            ThumbNail = thumbNail;
        }
    }
}