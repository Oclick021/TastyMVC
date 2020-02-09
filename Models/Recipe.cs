using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TastyMVC.Models
{
    public class Recipe
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string Title { get; set; }
        public int Portion { get; set; } = 1;
   public bool Published { get; set; }
        public Image Thumbnail { get; set; }
        public List<IngredientSize> Ingredients { get; set; } = new List<IngredientSize>();
        public Category Category { get; set; }
        public string CategoryID { get; set; }
        public List<RecipeStep> Steps { get; set; } = new List<RecipeStep>();
    }
}