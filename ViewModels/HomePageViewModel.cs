using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TastyMVC.Models;

namespace TastyMVC.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<Recipe> PublishedRecipes { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        
    }
}