using System;
using System.Collections.Generic;

namespace Ateliware.Server.Entities.Interfaces.Repositories
{
    public interface IRepositoryRepository
    {
        void Add(Repository repository);
        void Remove(int id);
        IEnumerable<Repository> GetAll();
        void SaveChanges();
        Repository FirstOrDefault(Func<Repository, bool> func);
    }
}
