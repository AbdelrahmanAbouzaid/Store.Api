using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey>
        where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreContext context;

        public GenericRepository(StoreContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trachChanges = false)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return trachChanges ?
                 (IEnumerable<TEntity>)await context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync()
                : (IEnumerable<TEntity>)await context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).AsNoTracking().ToListAsync();
            }

            return trachChanges ?
                 await context.Set<TEntity>().ToListAsync()
                : await context.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        public async Task<TEntity?> GetAsync(Tkey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return await context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).FirstOrDefaultAsync(p => p.Id == id as int?) as TEntity;

            }
            return await context.Set<TEntity>().FindAsync(id);
        }
        public async Task AddAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }
        public void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, Tkey> spec, bool trachChanges = false)
        {
            //return await SpecificationEvaluator.GetQuery(context.Set<TEntity>(), spec).ToListAsync();
            //return await context.Set<TEntity>().GetQuery(spec).ToListAsync();
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(ISpecification<TEntity, Tkey> spec)
        {
            //return await SpecificationEvaluator.GetQuery(context.Set<TEntity>(), spec).FirstOrDefaultAsync();
            //return await context.Set<TEntity>().GetQuery(spec).FirstOrDefaultAsync();
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        public async Task<int> CountAsync(ISpecification<TEntity, Tkey> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity, Tkey> spec)
        {
            return context.Set<TEntity>().GetQuery(spec);
        }

    
    }
}
