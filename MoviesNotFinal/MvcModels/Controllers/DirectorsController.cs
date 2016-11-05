using MvcModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcModels.Controllers
{
    public class DirectorsViewModel
    {
        public int Id { get; set; }
        public int Count { get; set; }
    }
    public class DirectorsController : Controller
    {
        //
        // GET: /Directors/
        public ActionResult Index()
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                ViewBag.Title = "רשימת במאים";
                var viewModel = (from mv in db.Movies
                        group mv by  mv.Director into director
                        select new DirectorsViewModel
                        {
                            Id = director.Key,
                            Count = director.Count()
                        }).ToList();
                ViewBag.WriterBookNum = viewModel;
                return View(db.Directors.ToList());
            }
        }

        //
        // GET: /Directors/Create
        public ActionResult Create()
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                ViewBag.Title = "הוספת במאי";
                return View();
            }
        }

        //
        // Post: /Directors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FullName")]Director dirDirectorToAdd)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                db.Directors.Add(dirDirectorToAdd);

                db.SaveChanges();

                return RedirectToAction("Index");;
            }
        }

        /// <summary>
        /// Post: Edit the writer details
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int? Id)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                Director dirToEdit = db.Directors.Find(Id);

                return View(dirToEdit);
            }
        }

        //
        // Post: /Directors/Edit/?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, FullName")]Director dirDirectorToEdit)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                Director dirObject = db.Directors.Find(dirDirectorToEdit.Id);
                dirObject.FullName = dirDirectorToEdit.FullName;

                db.SaveChanges();

                return RedirectToAction("Index"); ;
            }
        }

        /// <summary>
        /// Post: Delete the director
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int? Id)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                Director dirToDelete = db.Directors.Find(Id);

                return View(dirToDelete);
            }
        }


        //
        // Post: /Directors/Edit/?
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id, FullName")]Director dirToDelete)
        {
            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                Director dirDirectorToDelete = db.Directors.Find(dirToDelete.Id);
                db.Directors.Remove(dirDirectorToDelete);

                db.SaveChanges();

                return RedirectToAction("Index"); ;
            }
        }

        /// <summary>
        /// Post: Search the director
        /// </summary>
        /// <returns></returns>
        public ActionResult Search(string strDirectorName)
        {

            using (MoviesStoreDbContext db = new MoviesStoreDbContext())
            {
                if (strDirectorName == null)
                {
                    ViewBag.Title = "חיפוש במאי";
                    return View(db.Directors.ToList());
                }
                else
                {
                    return View(db.Directors.Where(x => x.FullName.Contains(strDirectorName)).ToList());
                }
            }
        }
	}
}