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
using WebGreenhouse.Areas.Admin.Controllers;

namespace WebGreenhouse.Controllers
{
    public class PostsController : BaseController
    {
        private GreenHouseDB db = new GreenHouseDB();

        // GET: Posts
        public ActionResult Index(int id)
        {
            if(TempData["result"] != null)
            {
                ViewBag.createSuccess = TempData["result"];
            }
            ViewBag.ID = id;
            var posts = db.Posts.Include(p => p.PostCategory).Include(p => p.User).Where(x=>x.CreatedByID == id);
            return View(posts.ToList());
        }

        // GET: Posts/Details/5
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

        // GET: Posts/Create
        public ActionResult Create(int id)
        {
            var dd = new SelectList(db.PostCategorys.Where(x => x.Status == true).Select(x => new { CategoryID = x.ID, NameCate = x.NameCate }), "CategoryID", "NameCate").ToList();
            ViewBag.CategoryID = new SelectList(db.PostCategorys.Where(x => x.Status == true).Select(x => new { CategoryID = x.ID, NameCate = x.NameCate }), "CategoryID", "NameCate");
            var user = db.Users.Where(x => x.ID == id && x.Status == true).ToList();
            ViewBag.CreatedByID = new SelectList(user, "ID", "Name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Alias,CategoryID,Images,Images2,Description,Content,HomeFlag,HotFlag,ViewCount,Tags,ActiveDate,IsSetTime,ImageFB,CreatedByID,StructureID,CreateDate,CreatedBy,UpdatedDate,UpdatedBy,MetaKeyword,MetaDescription,Status,Address,Price")] Post post)
        {
           
            if (ModelState.IsValid)
            {
                post.CreateDate = DateTime.Now;
                post.CreatedBy = "Member";
                var result = db.Posts.Add(post);
                if (result != null)
                {
                    db.SaveChanges();
                    TempData["result"] = "Tạo Mới Thành Công"; 
                    return RedirectToAction("Index","Posts",new { id = post.CreatedByID});
                }
                else
                {
                    return Redirect("/Posts/Create/" + post.CreatedByID);
                }
            }

            ViewBag.CategoryID = new SelectList(db.PostCategorys.Where(x => x.Status == true).Select(x => new { CategoryID = x.ID, NameCate = x.NameCate }), "ID", "NameCate",post.CategoryID);
            var user = db.Users.Where(x => x.ID == post.ID && x.Status == true).ToList();
            ViewBag.CreatedByID = new SelectList(user, "ID", "Name",post.CategoryID);
            return View(post);
        }

        // GET: Posts/Edit/5
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
            var user = db.Users.Where(x => x.ID == post.CreatedByID && x.Status == true).ToList();
            ViewBag.CreatedByID = new SelectList(user, "ID", "Name");
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Alias,CategoryID,Images,Images2,Description,Content,HomeFlag,HotFlag,ViewCount,Tags,ActiveDate,IsSetTime,ImageFB,CreatedByID,StructureID,CreateDate,CreatedBy,UpdatedDate,UpdatedBy,MetaKeyword,MetaDescription,Status,Address,Price")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.CreateDate = DateTime.Now;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                TempData["result"] = "Sửa Thành Công";
                return RedirectToAction("Index", "Posts", new { id = post.CreatedByID });
            }
            ViewBag.CategoryID = new SelectList(db.PostCategorys, "ID", "NameCate", post.CategoryID);
            var user = db.Users.Where(x => x.ID == post.CreatedByID && x.Status == true).ToList();
            ViewBag.CreatedByID = new SelectList(user, "ID", "Name",post.CategoryID);
            return View(post);
        }

        // GET: Posts/Delete/5
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

        // POST: Posts/Delete/5
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
