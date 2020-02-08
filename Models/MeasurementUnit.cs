using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TastyMVC.Models
{
    public class MeasurementUnit
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}