using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        private static GreenHouseDB db = new GreenHouseDB();
        private UserDAO() { }

        public static UserDAO getInstance()
        {
            if (instance == null)
            {
                instance = new UserDAO();
            }
            return instance;
        }
        public User GetByUserName(string username)
        {
            return db.Users.SingleOrDefault(x => x.UserName == username);
        }
        public List<User> GetById(int? id)
        {
            return db.Users.Where(x => x.ID == id && x.Status == true && x.Role.Equals("Member")).ToList();
        }
        public int Edit(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return user.ID;
        }
        public IEnumerable<User> ListAllPaging(string searchString, int page,int isize) 
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Role == "Member" && x.Name.Contains(searchString) || x.UserName.Contains(searchString));
            }
           return model.Where(x=>x.Role == "Member").OrderByDescending(x=>x.CreateDate).ToPagedList(page, isize); 
        }
        public int Login(string username, string password)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == username);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if(result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if(result.PassWord == password)
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
