using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Model.Base;
using RestWithASPNET.Model.Context;

namespace RestWithASPNET.Repository.Generic
{
    public class GenericRepository<T> : irepository<T> where T : BaseEntity
    { //tipo T e implementa a Irepository
        protected MySQLContext _context; 
        private DbSet<T> dataset;
        public GenericRepository(MySQLContext context){ 
            _context = context;
            dataset = _context.Set<T>();

        }
        public T create(T item)
        {
            try {
                dataset.Add(item);
                _context.SaveChanges();
                return item;

            }catch(Exception){
                throw;
            }
        }

        public void delete(long id)
        {
            var result = dataset.SingleOrDefault(p => p.id.Equals(id));
            if (result != null){
                try{
                    dataset.Remove(result);
                    _context.SaveChanges();
                }catch(Exception){
                    throw;
                }
            }
        }

        public bool exists(long id)
        {
            return dataset.Any(p => p.id.Equals(id));
        }

        public List<T> findall()
        {
            return dataset.ToList();
        }

        public T findbyid(long id)
        {
            return dataset.SingleOrDefault(p => p.id.Equals(id));
        }

        public T update(T item)
        {
            var result = dataset.SingleOrDefault(p => p.id.Equals(item.id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return(result);
                }
                catch(Exception)
                {
                    throw;
                }
            }
            else
            {
                return null;
            }
        }
    }
}