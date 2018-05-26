using System.Collections.Generic;
using Ateliware.Shared.ViewModel;

namespace Ateliware.Server.Entities.Interfaces.Services
{
    public interface IRepositoryService
    {
        IEnumerable<Repository> GetAll();
        void Add(ItemViewModel model);
        bool IsValid { get; set; }
        Dictionary<string,string> Errors { get; set; }
        void Save();
        void Remove(int id);
    }
}
