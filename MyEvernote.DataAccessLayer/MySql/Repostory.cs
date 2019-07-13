using MyEvernote.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.MySql
{
    class Repostory<T> : RepostoryBase, IRepository<T> where T : class
    {
        public int Delete(T obj)
        {
            throw new NotImplementedException();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public int Insert(T obj)
        {
            throw new NotImplementedException();
        }

        public List<T> List()
        {
            throw new NotImplementedException();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public int Update(T obj)
        {
            throw new NotImplementedException();
        }

        int IRepository<T>.Delete(T obj)
        {
            throw new NotImplementedException();
        }

        T IRepository<T>.Find(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        int IRepository<T>.Insert(T obj)
        {
            throw new NotImplementedException();
        }

        List<T> IRepository<T>.List()
        {
            throw new NotImplementedException();
        }

        List<T> IRepository<T>.List(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        IQueryable<T> IRepository<T>.ListQueryable()
        {
            throw new NotImplementedException();
        }

        int IRepository<T>.Save()
        {
            throw new NotImplementedException();
        }

        int IRepository<T>.Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
