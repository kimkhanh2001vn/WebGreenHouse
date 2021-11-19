using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CartDAO
    {
        private GreenHouseDB db = new GreenHouseDB();

        public Post GetById(int id)
        {
            return db.Posts.Find(id);
        }
    }
}
