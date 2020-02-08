using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TastyMVC.Models
{
    public class IngredientSize
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid Id { get; set; }
        public MeasurementUnit Unit { get; set; }
        public Guid UnitID { get; set; }
        public Guid RecipeId { get; set; }
        public Ingredient Ingredient { get; set; }
        public Guid IngredientID { get; set; }
        public decimal Amount { get; set; }

    }

}
