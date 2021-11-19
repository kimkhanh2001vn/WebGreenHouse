using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebGreenhouse.Common;
using WebGreenhouse.Models;

namespace WebGreenhouse.Controllers
{
    public class CartController : Controller
    {
        
        private GreenHouseDB db = new GreenHouseDB();

        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CommonStants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            if(TempData["Check"] != null)
            {
                ViewBag.err1 = TempData["Check"]; 
            }
            else if (TempData["success"] != null)
            {
                ViewBag.success = TempData["success"];
            }
            ViewBag.list = list;
            if (Session[CommonStants.ID_Customer] != null)
            {
                ViewBag.customer = CustomerDAO.getInstance().GetByCustomer((int)Session[CommonStants.ID_Customer]);
            }
            else
            {
                return Redirect("/login/login");
            }
            return View();
        }

        public ActionResult AddItem(int cartId, int quantity, string id)
        {
            var post = new CartDAO().GetById(cartId);
            var cart = Session[CommonStants.CartSession];
            if (cart != null && id != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.post.ID == cartId))
                {
                    foreach (var item in list)
                    {
                        if (item.post.ID == cartId)
                        {
                            TempData["Check"] = "Đã có Sản Phẩm trong giở, Hãy Gửi THông TIn Cho Chúng Tôi Sớm Nhất.";
                            Response.Redirect("/Cart/index");
                        }
                    }
                }
                else
                {
                    //tao moi doi tuong cartitem
                    var item = new CartItem();
                    item.post = post;
                    item.Quantity = quantity;
                    list.Add(item);
                }
            }
            else if (id == null)
            {
                Response.Redirect("/Login/login");
            }
            else
            {
                //tao moi doi tuong cartitem
                var item = new CartItem();
                item.post = post;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //gan session
                Session[CommonStants.CartSession] = list;
            }
            return RedirectToAction("Index");
        }
        public ActionResult PayMent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Payment(PaymentModel model)
        {
            var order = new Order();
            order.CustomerName = model.Name;
            order.CustomerAddress = model.Address;
            order.CustomerEmail = model.Email;
            order.CustomerMobile = model.Numberphone.ToString();
            order.CreatedDate = DateTime.Now;
            order.CustomerId = (int)Session[CommonStants.ID_Customer];
            order.Status = 0;

            try
            {
                var id = OrderDAO.getInstance().Insert(order);
                var cart = (List<CartItem>)Session[CommonStants.CartSession];
                var detailDAO = new OrderDetailsDAO();
                foreach(var item in cart)
                {
                    var detail = new OrderDetail();
                    detail.OrderID = id;
                    detail.Price = (decimal)item.post.Price;
                    detail.Quantity = item.Quantity;
                    detail.PostID = item.post.ID;
                    detailDAO.Insert(detail);
                    TempData["success"] = "Thông Tin Của Bạn Đã Được Lưu Trữ. Chúng Tôi Sẽ Liên Hệ Với Bạn Nhanh Nhất.";
                    Session[CommonStants.CartSession] = null;
                }
            }
            catch (Exception ex)
            {
                return Redirect("/");
                throw;
            }
            return RedirectToAction("Index","Cart");
        }
        public JsonResult DeleteAll()
        {
            Session[CommonStants.CartSession] = null;
            return Json(new { status = true });
        }
        public JsonResult Delete(int id)
        {
            var sessionCart = (List<CartItem>)Session[CommonStants.CartSession];
            sessionCart.RemoveAll(x => x.post.ID == id);
            Session[CommonStants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
    }
}
