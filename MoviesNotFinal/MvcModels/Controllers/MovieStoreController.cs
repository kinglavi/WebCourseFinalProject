using MvcModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcModels.Controllers
{
    public class MoviesStoreController : Controller
    {
        //
        // GET: /MoviesStore/
        public ActionResult Index()
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                ViewBag.Title = "מפת ספריות";
                return PartialView(db.MoviesStores.ToList());
            }
        }
        //
        // GET: /MoviesStore/
        public ActionResult Create()
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                ViewBag.Title = "הוספת ספריה";
                return PartialView();
            }
        }

        //
        // Post: /MoviesStore/?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind (Include = "MovieStoreName")]MoviesStore mvsNewMovieStores)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                db.MoviesStores.Add(mvsNewMovieStores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        /// <summary>
        /// Edit: Edit the MoviesStore details
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int? Id)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                MoviesStore lib = db.MoviesStores.Find(Id);
                return PartialView(lib);
            }
        }

        /// <summary>
        /// Post: Edit the MoviesStore details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, MovieStoreName")]MoviesStore mvsNewMovieStores)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                MoviesStore mvsMovieStoreWanted = db.MoviesStores.Find(mvsNewMovieStores.Id);
                mvsMovieStoreWanted.MovieStoreName = mvsNewMovieStores.MovieStoreName;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Post: Delete the MoviesStore
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int? Id)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                MoviesStore mvsMovieStoreToDelete = db.MoviesStores.Find(Id);
                return PartialView(mvsMovieStoreToDelete);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id, MovieStoreName")]MoviesStore mvsToDelete)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                MoviesStore libLibraryToDelete = db.MoviesStores.Find(mvsToDelete.Id);
                db.MoviesStores.Remove(libLibraryToDelete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Post: Show all the movies in the MoviesStores
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MoviesInMoviesStore()
        {
            return PartialView();
        }

        /// <summary>
        /// Post: Edit all the movies in specific MoviesStore
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBooksInMoviesStore(int Id)
        {
            return PartialView();
        }

        ///// <summary>
        ///// post: search the movie
        ///// </summary>
        ///// <returns></returns>
        //public actionresult search(string strmoviesstoreaddress)
        //{

        //    using (moviesstoredbcontext db = new moviesstoredbcontext())
        //    {
        //        if (strmoviesstoreaddress == null)
        //        {
        //            viewbag.title = "חיפוש סרטים";
        //            return view(db.moviesstores.tolist());
        //        }
        //        else
        //        {
        //            return view(db.moviesstores.where(x => x.moviesstoreaddress.contains(strmoviesstoreaddress)).tolist());
        //        }
        //    }
        //}
	}
}