using MvcModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcModels.Controllers
{
    public class MoviesViewModel
    {
        public int MovieId { get; set; }
        public string Director { get; set; }
    }
    public class MoviesController : Controller
    {
        //
        // GET: /Movies/
        public ActionResult Index(string strCategory)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                var viewModel =
                    from director in db.Directors
                    join movie in db.Movies on director.Id equals movie.Director
                    select new MoviesViewModel { MovieId = movie.Id, Director = director.FullName };
                ViewBag.Directors = viewModel.ToList();

                if (strCategory != null)
                {
                    ViewBag.Category = "סרטים " + strCategory;
                    return View(db.Movies.Where(x => String.Equals(x.Genre, strCategory)).ToList());
                }
                else 
                {
                    ViewBag.Category = "כל הסרטים";

                    return View(db.Movies.ToList());
                }
            }
        }

        //
        // GET: /Books/Create
        public ActionResult Create()
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                ViewBag.Title = "הוספת סרט";
                ViewBag.Directors = db.Movies.ToList();
                return View();
            }
        }

        //
        // Post: /Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieName, Director, Genre, PublishYear")]Movie mvNewMovie)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                mvNewMovie.AddedDate = DateTime.Now;
                db.Movies.Add(mvNewMovie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Get: Edit the book details
        /// </summary>
        public ActionResult Edit(int? id)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                Movie bkToEdit = db.Movies.Find(id);
                return View(bkToEdit);
            }
        }

        /// <summary>
        /// Post: Edit the movie details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, MovieName, Director, Genre, PublishYear")]Movie mvToEdit)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                Movie mvWantToEdit = db.Movies.Find(mvToEdit.Id);
                mvWantToEdit.MovieName = mvToEdit.MovieName;
                mvWantToEdit.Director = mvToEdit.Director;
                mvWantToEdit.Genre = mvToEdit.Genre;
                mvWantToEdit.PublishYear = mvToEdit.PublishYear;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Post: Delete the movie
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                ViewBag.Title = "מחיקת סרט";
                Movie bkToDelete = db.Movies.Find(id);
                return View(bkToDelete);
            }
        }

        /// <summary>
        /// Post: Delete the book
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id, MovieName, Director, Genre, PublishYear")]Movie mvToDelete)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                Movie mvWantToDelete = db.Movies.Find(mvToDelete.Id);
                db.Movies.Remove(mvWantToDelete);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Post: Search the movie
        /// </summary>
        /// <returns></returns>
        public ActionResult Search(string strMovieName, string strMovieGenre, string strMoviePublishYear)
        {

            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                if (strMovieName == null && strMovieGenre == null && strMoviePublishYear == null) 
                { 
                    ViewBag.Title = "חיפוש ספר";
                    return View(db.Movies.ToList());
                }
                else
                {
                    return View(db.Movies.Where(x => x.MovieName.Contains(strMovieName) &&
                        x.PublishYear.ToString().Contains(strMoviePublishYear) &&  
                        x.Genre.Contains(strMovieGenre)).ToList());
                }
            }
        }
	}
}