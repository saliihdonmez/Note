using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.Entity
{
  public  class RepositoryBase
    {
        protected static DatabaseContext context;
        private static object _lock = new object();

        protected RepositoryBase()
        {
            context = CreateContext();
        }
        private static DatabaseContext CreateContext()
        {
            if (context == null)
            {
                lock (_lock)
                {
                    if (context == null)
                    {
                        context = new DatabaseContext();
                    }
                }
            }
            return context;
        }
    }
}
