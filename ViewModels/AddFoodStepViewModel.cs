using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TastyMVC.ViewModels
{
    public class AddFoodStepViewModel
    {
        public int Order { get; set; } = 1;
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFile Image { get; set; }
        public Guid RecipeID { get; set; }
        public AddFoodStepViewModel()
        {

        }
        public AddFoodStepViewModel(Guid recipeID)
        {
            RecipeID = recipeID;
        }
    }
}