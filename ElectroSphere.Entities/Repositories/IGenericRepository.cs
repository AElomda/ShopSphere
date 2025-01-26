﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ElectroSphere.Entities.Repositories
{
    public interface IGenericRepository<T> where T : class 
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> perdicate, string? Includeword);    
        T GetFirstorDefault(Expression<Func<T, bool>> perdicate, string? Includeword);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}
