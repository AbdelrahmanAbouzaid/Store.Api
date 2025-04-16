using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<int> CountAsync(ISpecification<TEntity, Tkey> spec);
        Task<IEnumerable<TEntity>> GetAllAsync(bool trachChanges = false);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity,Tkey> spec, bool trachChanges = false);
        Task<TEntity?> GetAsync(Tkey id);
        Task<TEntity?> GetAsync(ISpecification<TEntity, Tkey> spec);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
