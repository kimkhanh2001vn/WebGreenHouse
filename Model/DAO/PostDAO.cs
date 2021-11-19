using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Model.DAO
{
    public class PostDAO
    {
        private static PostDAO instance = null;
        private static GreenHouseDB db = new GreenHouseDB();
        private PostDAO() { }

        public static PostDAO getInstance()
        {
            if (instance == null)
            {
                instance = new PostDAO();
            }
            return instance;
        }
        public bool ChangStatus(int id)
        {
            var post = db.Posts.Find(id);
            post.Status = !post.Status;
            return !post.Status;
        }
        public IEnumerable<Post> ListAllPaging(string searchString, int page, int isize)
        {
            IQueryable<Post> model = db.Posts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x=>x.Name.Contains(searchString) || x.Alias.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, isize);
        }
        public bool Insert(Post post)
        {
            if(post != null)
            {
                var pList = new Post();
                pList.ID = post.ID;
                pList.CategoryID = post.CategoryID;
                pList.Name = post.Name;
                pList.CategoryID = post.CategoryID;
                pList.Alias = post.Alias;
                pList.Images = post.Images;
                pList.Description = post.Description;
                pList.Content = post.Content;
                pList.HomeFlag = true;
                pList.HotFlag = true;
                pList.ViewCount = post.ViewCount;
                pList.Tags = post.Tags;
                pList.ActiveDate = post.ActiveDate;
                pList.IsSetTime = true;
                pList.ImageFB = post.ImageFB;
                pList.CreatedByID = post.CreatedByID;
                pList.StructureID = post.StructureID;
                pList.CreateDate = DateTime.Now;
                pList.CreatedBy = "Member";
                pList.UpdatedBy = post.UpdatedBy;
                pList.UpdatedDate = post.UpdatedDate;
                pList.MetaKeyword = post.MetaKeyword;
                pList.MetaDescription = post.MetaDescription;
                pList.Status = true;
                pList.Price = post.Price;
                pList.Address = post.Address;
                db.Posts.Add(post);
                db.SaveChanges();

                return true;
            }
            return false;
        }
    }
}
