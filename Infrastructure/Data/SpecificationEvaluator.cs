using Core.Entities;
using Core.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity :BaseEntity
    {
public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            //asuume that we have Product Entity 
             var query = inputQuery; //query = IQueryable<Product>
            if(spec.Criteria != null) //if the Criteria not equal null we filter with that createria
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.OrderBy != null) //then add orderBy
            {
                query = query.OrderBy(spec.OrderBy);
            } 
            if (spec.OrderByDes != null)
            {
                query = query.OrderByDescending(spec.OrderByDes);
            }
            if (spec.IsPaginEnable)  //Check if The isPaging Enable 
            {
               query= query.Skip(spec.Skip).Take(spec.Take);
            }
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }
    }
}
