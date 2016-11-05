using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcModels.Models
{
    public class MoviesStore
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("שם החנות סרטים")]
        public string MovieStoreName { get; set; }
        
        public virtual List<Movie> MoviesInStore { get; set; }
    }
}