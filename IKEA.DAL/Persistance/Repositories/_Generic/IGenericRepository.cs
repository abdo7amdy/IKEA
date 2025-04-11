using IKEA.DAL.Models;
using IKEA.DAL.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories._Generic
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IQueryable<T> GetAll(bool WithNoTracking = true);
        T? GetById(int id);

        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);

    }
}
