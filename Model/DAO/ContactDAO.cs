using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ContactDAO
    {
        private GreenHouseDB db = null;
        public ContactDAO()
        {
            db = new GreenHouseDB();
        }
        public Contact GetActiveContact()
        {
            return db.Contacts.Single(x => x.Status == true);
        }
    }
}
