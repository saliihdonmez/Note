﻿using MyEvernote.Core.DataAccess;
using MyEverNote.Common;
using MyEverNote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.Entity
{
  public  class Repository<T> : RepositoryBase, IDataAccess<T> where T:class
    {
        private DbSet<T> _objectSet;

        public Repository()
        {
           
            _objectSet = context.Set<T>();
        }

        public List<T> List()
        {
            return _objectSet.ToList();
        }

        public IQueryable<T> ListQueryable()
        {
          return _objectSet.AsQueryable<T>();
        }

        public List<T> List(Expression<Func<T,bool>> where)
        {

            return _objectSet.Where(where).ToList();
        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);

            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
                DateTime now = DateTime.Now;

                o.CreateOn = now;
                o.Modifiedon = now;
                o.ModifiedUsername = App.Common.GetCurrentUsername(); //Todo : işlem yapan kullanıcı adı yazılmalı
            }

            return Save();
        }

        public int Update(T obj)
        {
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;

                o.Modifiedon = DateTime.Now;
                o.ModifiedUsername = App.Common.GetCurrentUsername(); //Todo : işlem yapan kullanıcı adı yazılmalı
            }

            return Save();
        }

        public int Delete (T obj)
        {
            //if (obj is MyEntityBase)
            //{
            //    MyEntityBase o = obj as MyEntityBase;

            //    o.Modifiedon = DateTime.Now;
            //    o.ModifiedUsername = App.Common.GetUsername(); //Todo : işlem yapan kullanıcı adı yazılmalı
            //}

            _objectSet.Remove(obj);
            return Save();
        }

        public int Save()
        {

            return context.SaveChanges();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }

        List<T> IDataAccess<T>.List()
        {
            throw new NotImplementedException();
        }

        List<T> IDataAccess<T>.List(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        int IDataAccess<T>.Insert(T obj)
        {
            throw new NotImplementedException();
        }

        int IDataAccess<T>.Update(T obj)
        {
            throw new NotImplementedException();
        }

        int IDataAccess<T>.Delete(T obj)
        {
            throw new NotImplementedException();
        }

        int IDataAccess<T>.Save()
        {
            throw new NotImplementedException();
        }

        T IDataAccess<T>.Find(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }
    }
}
