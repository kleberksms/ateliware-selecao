using System;
using System.Collections.Generic;
using System.Linq;
using Ateliware.Server.Entities.Interfaces.Repositories;
using Ateliware.Server.Repository.Context;

namespace Ateliware.Server.Repository
{
    public class RepositoryRepository : IRepositoryRepository
    {
        private readonly AteliwareContext _context;
        public RepositoryRepository(AteliwareContext context)
        {
            _context = context;
        }
        public void Add(Entities.Repository repository)
        {
            _context.Add(repository);
        }

        public void Remove(int id)
        {
            var item = _context.Repositories.FirstOrDefault(c => c.Id.Equals(id));
            _context.Remove(item ?? throw new InvalidOperationException());
        }

        public IEnumerable<Entities.Repository> GetAll()
        {
            return _context.Repositories;
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Entities.Repository FirstOrDefault(Func<Entities.Repository, bool> func)
        {
            return _context.Repositories.FirstOrDefault(func);
        }
    }
}
