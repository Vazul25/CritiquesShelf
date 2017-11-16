using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CritiquesShelfBLL.Entities;
namespace CritiquesShelfBLL.RepositoryInterfaces
{
    public class RepositoryBase
    {
        protected readonly CritiquesShelfDbContext _context;

        public RepositoryBase(CritiquesShelfDbContext context)
        {
            _context = context;
        }

        public void Insert<T>(T entity, bool doSave = false) where T : class
        {
            _context.Set<T>().Add(entity);
            _saveChanges(doSave);

        }

        public T Find<T>(long id) where T : PersistentEntity
        {
            return _context.Set<T>().Find(id);
        }

        public void Delete<T>(long id, bool doSave = false) where T : PersistentEntity
        {
            var entity = Find<T>(id);
            _context.Set<T>().Remove(entity);
            _saveChanges(doSave);
        }

        public List<T> List<T>() where T : class
        {
            return _context.Set<T>().ToList();
        }

        public List<T> List<T>(Expression<Func<T, bool>> predicate) where T : class
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
