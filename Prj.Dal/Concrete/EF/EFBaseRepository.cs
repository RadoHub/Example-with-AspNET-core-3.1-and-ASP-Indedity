using Microsoft.EntityFrameworkCore;
using Prj.Dal.Abstract;
using Prj.Ent.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Prj.Dal.Concrete.EF
{
    public class EFBaseRepository<T, TContext> : IRepository<T> where T : class, IEntity, new() where TContext : DbContext, new()
    {
        public void CreateObj(T entity)
        {
            using (TContext con = new TContext())
            {
                var createdObj = con.Entry(entity);
                createdObj.State = EntityState.Added;
                con.SaveChanges();
            }
        }

        public void DeleteObj(T entity)
        {
            using (TContext con = new TContext())
            {
                var deletedObj = con.Entry(entity);
                deletedObj.State = EntityState.Deleted;
                con.SaveChanges();
            }
        }

        public IEnumerable<T> GetAllObjs(Expression<Func<T, bool>> filter = null)
        {
            using (TContext con = new TContext())
            {
                return filter == null ? con.Set<T>().ToList() : con.Set<T>().Where(filter).ToList();
            }
        }

        public T GetObjById(int id)
        {
            using (TContext con = new TContext())
            {
                return con.Set<T>().Find(id);
            }
        }

        public T GetOneObj(Expression<Func<T, bool>> filter=null)
        {
            using (TContext con = new TContext())
            {
                return filter==null ? null : con.Set<T>().Where(filter).FirstOrDefault();
            }
        }


        public virtual void UpdateObj(T entity)
        {
            using (TContext con = new TContext())
            {
                var updatedObj = con.Entry(entity);
                updatedObj.State = EntityState.Modified;
                con.SaveChanges();
            }
        }
    }
}
