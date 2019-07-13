using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.Entity
{
  public  class RepositoryBase
    {
        protected static DatabaseContext db;
        private static object _lock = new object();

        protected RepositoryBase()
        {
            db = CreateContext();
        }
        private static DatabaseContext CreateContext()
        {
            if (db == null)
            {
                lock (_lock)
                {
                    if (db == null)
                    {
                        db = new DatabaseContext();
                    }
                }
            }
            return db;
        }
    }
}
