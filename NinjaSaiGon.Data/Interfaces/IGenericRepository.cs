using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSaiGon.Data.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(object id);

        Task Create(TEntity entity);

        Task Update(object id, TEntity entity);

        Task Delete(object id);
    }
}
