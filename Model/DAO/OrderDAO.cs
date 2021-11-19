using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static GreenHouseDB db = new GreenHouseDB();
        private OrderDAO() { }

        public static OrderDAO getInstance()
        {
            if (instance == null)
            {
                instance = new OrderDAO();
            }
            return instance;
        }
        public int Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.OrderID;
        }
        public void CheckSuccess(int orderid, int ?value)
        {
            if(value == 1)
            {
                Order order = db.Orders.Find(orderid);
                order.Status = 3;
                db.SaveChanges();
            }
            
        }
        public void CheckPhone(int orderid, int ?value)
        {
            if(value == 2)
            {
                Order order = db.Orders.Find(orderid);
                order.Status = 1;
                db.SaveChanges();
            }
        }
        public List<Order> GetById(int? id)
        {
            return db.Orders.Where(x => x.CustomerId == id).ToList();
        }
    }
}
