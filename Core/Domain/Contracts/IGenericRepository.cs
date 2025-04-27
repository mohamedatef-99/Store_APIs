using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<int> CountAsync(ISpecifications<TEntity, Tkey> specifications);
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> specifications, bool trackChanges = false);
        Task<TEntity?> GetAsync(Tkey id);
        Task<TEntity?> GetAsync(ISpecifications<TEntity, Tkey> specifications);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
