using Prj.Ent.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Prj.Dal.Abstract
{
    public interface IRepository<T> where T: class, IEntity, new()
    {
        void CreateObj(T entity);
        void DeleteObj(T entity);
        void UpdateObj(T entity);

        IEnumerable<T> GetAllObjs(Expression<Func<T, bool>> filter = null);
        T GetOneObj(Expression<Func<T, bool>> filter=null);
        T GetObjById(int id);
    }
}
