using System;
using System.ComponentModel.DataAnnotations;

namespace TastyMVC.Models
{
    public class Ingredient
    {
        [Key]

        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}