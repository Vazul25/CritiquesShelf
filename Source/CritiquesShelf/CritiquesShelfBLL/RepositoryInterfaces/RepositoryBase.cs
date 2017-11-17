using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CritiquesShelfBLL.Entities;
namespace CritiquesShelfBLL.RepositoryInterfaces
{
    public class RepositoryBase<T> where T: class
    {
        protected readonly CritiquesShelfDbContext _context;

        public RepositoryBase(CritiquesShelfDbContext context)
        {
            _context = context;
        }

        public void Insert (T entity, bool doSave = false)  
        {
            _context.Set<T>().Add(entity);
            _saveChanges(doSave);

        }

        public T Find(long id)  
        {
            return _context.Set<T>().Find(id);
        }

        public void Delete(long id, bool doSave = false)  
        {
            var entity = Find(id);
            _context.Set<T>().Remove(entity);
            _saveChanges(doSave);
        }

        public List<T> List() 
        {
            return _context.Set<T>().ToList();
        }

        public List<T> List(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>()
                           .Where(predicate)
                           .ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        private void _saveChanges(bool doSave) {
            if (doSave) 
            {
                SaveChanges();
            }
        }

    }
}
