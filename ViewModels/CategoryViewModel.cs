using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TastyMVC.Data;
using TastyMVC.Models;

namespace TastyMVC.ViewModels
{
    public class CategoryViewModel
    {
        public Dictionary<Category, int> Categories { get; set; }
        public string SelectedCategory { get; set; }

        public CategoryViewModel()
        {
            Categories = new Repository().GetCategoriesWithCount();
        }
    }
}