using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TastyMVC.Models
{
    public class IngredientSize
    {
      public  enum MeasurementUnits
        {
            teaspoon,
            tablespoon,
            fluid_ounce,
            cup,
            pint,
            ml,
            l,
            dl,
            pound,
            ounce,
            mg,
            g,
            kg
        }
        [Key]
        public Guid Id { get; set; }
        public MeasurementUnits Unit { get; set; }
        public Ingredient Ingredient { get; set; }

    }

}
