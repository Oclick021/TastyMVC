using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TastyMVC.Models;

namespace TastyMVC.Data
{
    public class Repository
    {
        private ApplicationDbContext dbContet; 
        public Repository()
        {
            dbContet = new ApplicationDbContext();
        }

        public IEnumerable<Ingredient> GetAllIngredients()
        {
            var allIngredients = dbContet.Ingredients.GroupBy(x => x.Name).Select(x => x.FirstOrDefault());
            return allIngredients;
        }
        public IEnumerable<MeasurementUnit> GetAllUnits()
        {
           return dbContet.MeasurementUnits;
        }
    }
}