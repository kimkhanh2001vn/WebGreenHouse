using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace WebGreenhouse.Areas.Admin.Controllers
{
    public class OrderDetailsController : Controller
    {
        private GreenHouseDB db = new GreenHouseDB();

        // GET: Admin/OrderDetails
        public ActionResult Index()
        {
            var orderDetails = db.OrderDetails.Include(o => o.OrderID).Include(o => o.PostID);
            return View(orderDetails.ToList());
        }

        // GET: Admin/OrderDetails/Details/5
        public ActionResult Details(int? id,int? postid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id, postid);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.OrderID = new SelectList(db.Orders, "ID", "CustomerName");
            ViewBag.PostID = new SelectList(db.Posts, "ID", "Name");
            return View();
        }

        // POST: Admin/OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,PostID,Quantity,Price,ColorId,SizeId")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderID = new SelectList(db.Orders, "ID", "CustomerName", orderDetail.OrderID);
            ViewBag.PostID = new SelectList(db.Posts, "ID", "Name", orderDetail.PostID);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Edit/5
        public ActionResult Edit(int? id, int? postid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id,postid);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.Orders, "ID", "CustomerName", orderDetail.OrderID);
            ViewBag.PostID = new SelectList(db.Posts, "ID", "Name", orderDetail.PostID);
            return View(orderDetail);
        }

        // POST: Admin/OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,PostID,Quantity,Price,ColorId,SizeId")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderID = new SelectList(db.Orders, "ID", "CustomerName", orderDetail.OrderID);
            ViewBag.PostID = new SelectList(db.Posts, "ID", "Name", orderDetail.PostID);
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Delete/5
        public ActionResult Delete(int? id,int postid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id, postid);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: Admin/OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
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
