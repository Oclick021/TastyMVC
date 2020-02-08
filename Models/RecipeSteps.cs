using System;
using System.ComponentModel.DataAnnotations;

namespace TastyMVC.Models
{
    public class RecipeSteps
    {
        [Key]
        public Guid ID { get; set; }
        public string Description { get; set; }
        public Image Image { get; set; }
        public int Order { get; set; }

    }
}