using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MvcModels.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("במאי")]
        public int Director { get; set; }
        [Required]
        [DisplayName("שחקנים")]
        public int Actors { get; set; }
        [Required]
        [DisplayName("שם הסרט")]
        public string MovieName { get; set; }
        [Required]
        [DisplayName("ז'אנר")]
        public string Genre { get; set; }
        [Required]
        [DisplayName("שנת הוצאה לאור")]
        public int PublishYear { get; set; }
        [Required]
        [DisplayName("תאריך הוספה")]
        public DateTime AddedDate { get; set; }
        [DisplayName("דירוג")]
        public double Rate { get; set; }
    }
}