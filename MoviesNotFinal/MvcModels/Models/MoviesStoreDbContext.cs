using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MvcModels.Models
{
    public class MoviesStoreDbContext: DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MoviesStore> MoviesStores { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<MovieCustomerManyToMany> MoviesBoughtByCustomers { get; set; }
        public DbSet<User> Users { get; set; }
    }

    public static class HtmlExtensions
    {
        public static string JsonSerialize(this HtmlHelper htmlHelper, object value)
        {
            return new JavaScriptSerializer().Serialize(value);
        }
    }
}