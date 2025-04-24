using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity, Tkey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, Tkey> specifications)
            where TEntity : BaseEntity<Tkey>
        {
            var query = inputQuery;
            if (specifications.Criteria is not null)
                query.Where(specifications.Criteria);
            
            query = specifications.IncludeExpressions.Aggregate(query, (currenyQuery, includeExperssion) => currenyQuery.Include(includeExperssion));
            
            return query;
        }
    }
}
