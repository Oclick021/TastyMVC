using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TastyMVC.Models
{
    public class Recipe
    {
        [Key]

        public Guid Id { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string Title { get; set; }
        public string portion { get; set; }
        public List<IngredientSize> Ingredients { get; set; }

        public List<RecipeSteps> Steps { get; set; }
        public Image Thumbnail { get; set; }
    }
}