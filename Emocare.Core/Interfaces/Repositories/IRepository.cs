﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emocare.Domain.Interfaces.Repositories
{ 
        public interface IRepository<T> where T : class
        {
            Task<IEnumerable<T>> GetAll();
            Task<T?> GetById(Guid id);
            Task<bool> Add(T entity);
            Task<bool> Update(T entity);
            Task<bool> Delete(Guid id);
        }
    }

