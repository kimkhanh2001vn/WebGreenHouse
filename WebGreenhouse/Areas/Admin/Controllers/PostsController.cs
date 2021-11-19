using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;
using PagedList;

namespace WebGreenhouse.Areas.Admin.Controllers
{
    public class PostsController : BaseController
    {
        private GreenHouseDB db = new GreenHouseDB();

        // GET: Admin/Posts
        public ActionResult Index(string searchString,int page = 1,int isize = 12)
        {
            IQueryable<Post> posts = db.Posts.Include(p => p.PostCategory).Include(p => p.User);
            if (!string.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(x => x.Name.Contains(searchString) || x.Alias.Contains(searchString));
            }
            ViewBag.model = posts.OrderByDescending(x => x.CreateDate).ToPagedList(page, isize);
            return View();
        }

        // GET: Admin/Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Posts/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.PostCategorys, "ID", "NameCate");
            ViewBag.CreatedByID = new SelectList(db.Users, "ID", "UserName");
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Alias,CategoryID,Images,Images2,Description,Content,HomeFlag,HotFlag,ViewCount,Tags,ActiveDate,IsSetTime,ImageFB,CreatedByID,StructureID,CreateDate,CreatedBy,UpdatedDate,UpdatedBy,MetaKeyword,MetaDescription,Status,Address,Price")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.PostCategorys, "ID", "NameCate", post.CategoryID);
            ViewBag.CreatedByID = new SelectList(db.Users, "ID", "UserName", post.CreatedByID);
            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.PostCategorys, "ID", "NameCate", post.CategoryID);
            ViewBag.CreatedByID = new SelectList(db.Users, "ID", "UserName", post.CreatedByID);
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Alias,CategoryID,Images,Images2,Description,Content,HomeFlag,HotFlag,ViewCount,Tags,ActiveDate,IsSetTime,ImageFB,CreatedByID,StructureID,CreateDate,CreatedBy,UpdatedDate,UpdatedBy,MetaKeyword,MetaDescription,Status,Address,Price")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.PostCategorys, "ID", "NameCate", post.CategoryID);
            ViewBag.CreatedByID = new SelectList(db.Users, "ID", "UserName", post.CreatedByID);
            return View(post);
        }

        // GET: Admin/Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
