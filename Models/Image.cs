﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TastyMVC.Models
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}