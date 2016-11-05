﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcModels.Models
{
    public class Director
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("שם הבמאי")]
        public string FullName { get; set; }
    }
}