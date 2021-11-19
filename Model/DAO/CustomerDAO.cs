using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CustomerDAO
    {
        private static CustomerDAO instance = null;
        private static GreenHouseDB db = new GreenHouseDB();
        private CustomerDAO() { }

        public static CustomerDAO getInstance()
        {
            if (instance == null)
            {
                instance = new CustomerDAO();
            }
            return instance;
        }
        public bool Insert(Customer customer)
        {
            if (customer != null)
            {
                var pList = new Customer();
                pList.CustomerID = customer.CustomerID;

                db.Entry(pList).State = EntityState.Modified;
                db.SaveChanges();

                return true;
            }
            return false;
        }
        public bool Update (Customer model)
        {
            try
            {
                var customer = db.Customers.Find(model.CustomerID);
                customer.CustomerName = model.CustomerName;
                customer.CustomerEmail = model.CustomerEmail;
                customer.CustomerAddress = model.CustomerAddress;
                customer.CustomerWork = model.CustomerWork;
                customer.NumberPhone = model.NumberPhone;
                customer.Description = model.Description;
                if (string.IsNullOrEmpty(model.PassWord))
                {
                    customer.PassWord = model.PassWord;
                }
                customer.LinkFacebook = model.LinkFacebook;
                customer.LinkZalo = model.LinkZalo;
                customer.Avata = "abc";
                customer.CreateDate = DateTime.Now;
                customer.Status = true;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Customer GetByUserName(string username)
        {
            return db.Customers.SingleOrDefault(x => x.UserName == username);
        }
        public List<Customer> GetById(int? id)
        {
            return db.Customers.Where(x=>x.CustomerID == id && x.Status == true).ToList();
        }
        public List<Customer> GetByCustomer(int id)
        {
            return db.Customers.Where(x => x.CustomerID == id && x.Status == true).ToList();
        }
        public bool Check(string email , string username)
        {
            var result = db.Customers.Where(x => x.CustomerEmail == email || x.UserName == username).ToList();
            if(result.Count > 0)
            {
                return false;
            }
            else if(string.IsNullOrEmpty(email))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            return true;
        }
        public bool ConfirmPassword(string pass,string confirmPass)
        {
            if(string.Compare(pass,confirmPass) == 0)
            {
                return true;
            }
            else if (string.IsNullOrEmpty(pass))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(confirmPass))
            {
                return false;
            }
            return false;
        }
        public int Login(string username, string password)
        {
            var result = db.Customers.SingleOrDefault(x => x.UserName == username);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.PassWord == password)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
        }
    }
}
