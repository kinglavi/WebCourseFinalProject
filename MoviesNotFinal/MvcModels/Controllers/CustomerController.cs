using MvcModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcModels.Controllers
{
    public class CustomersViewModel
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
    }
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
        public ActionResult Index()
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                return View(db.Customers.ToList());
            }
        }

        // GET: /Customer/BoughtMovies
        public ActionResult BuyAMovie(int? Id)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                ViewBag.WantedMovieId = Id;
                return View(db.Customers.ToList());
            }
        }

        // GET: /Customer/BoughtMovies
        public ActionResult LoanTheMovie(int CustomerId, int MovieId)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                MovieCustomerManyToMany mvcst = new MovieCustomerManyToMany();
                mvcst.CustomerId = CustomerId;
                mvcst.MovieId = MovieId;

                db.MoviesBoughtByCustomers.Add(mvcst);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        // GET: /Customer/BoughtMovies
        public ActionResult BoughtMovies()
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                var manyToMany = from mtm in db.MoviesBoughtByCustomers
                                 join mv in db.Movies on mtm.MovieId equals mv.Id
                                 join cst in db.Customers on mtm.CustomerId equals cst.Id
                                 select new CustomersViewModel { Id = mtm.CustomerId, MovieName = mv.MovieName };

                ViewBag.MovieBought = manyToMany.ToList();

                return View(db.Customers.ToList());
            }
        }

        //
        // GET: /Customer/Create
        public ActionResult Create()
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                ViewBag.Title = "הוספת לקוח";
                return View();
            }
        }

        //
        // Post: /Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id ,FullName, BoughtMovies, Address")]Customer cstNewCustomer)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                db.Customers.Add(cstNewCustomer);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Post: Edit the customer details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? Id)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                ViewBag.Title = "עריכת לקוח";
                return View(db.Customers.Find(Id));
            }
        }

        //
        // Post: /Customer/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, FullName, BoughtMovies, Address")]Customer cstCustToEdit)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                Customer cstCustomer = db.Customers.Find(cstCustToEdit.Id);
                cstCustomer.Address = cstCustToEdit.Address;
                cstCustomer.FullName = cstCustToEdit.FullName;
                cstCustomer.BoughtMovies = cstCustToEdit.BoughtMovies;
                
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Post: Delete the customer
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int? Id)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                ViewBag.Title = "מחיקת לקוח";
                Customer cstCustomerToDelete = db.Customers.Find(Id);
                return View(cstCustomerToDelete);
            }
        }

        /// <summary>
        /// Post: Delete the customer
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id, FullName, BoughtMovies, Address")]Customer cstCustToDelete)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                Customer cstCustomerToDelete = db.Customers.Find(cstCustToDelete.Id);
                db.Customers.Remove(cstCustomerToDelete);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Post: Search the customer
        /// </summary>
        /// <returns></returns>
        public ActionResult Search(string strCustomerName, string strCustomerId, string strCustomerAddress)
        {

            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                if (strCustomerName == null && strCustomerId == null && strCustomerAddress == null)
                {
                    ViewBag.Title = "חיפוש לקוח";
                    return View(db.Customers.ToList());
                }
                else
                {
                    return View(db.Customers.Where(x => x.Id.ToString().Contains(strCustomerId) &&
                        x.FullName.Contains(strCustomerName) &&
                        x.Address.Contains(strCustomerAddress)).ToList());
                }
            }
        }
	}
}