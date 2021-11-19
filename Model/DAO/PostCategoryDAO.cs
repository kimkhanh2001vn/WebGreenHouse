using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class PostCategoryDAO
    {
        private static PostCategoryDAO instance = null;
        private static GreenHouseDB db = new GreenHouseDB();
        private PostCategoryDAO() { }

        public static PostCategoryDAO getInstance()
        {
            if (instance == null)
            {
                instance = new PostCategoryDAO();
            }
            return instance;
        }
        public IEnumerable<PostCategory> ListAllPaging(string searchString, int page, int isize)
        {
            IQueryable<PostCategory> model = db.PostCategorys;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.NameCate.Contains(searchString) || x.Alias.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, isize);
        }

    }
}
