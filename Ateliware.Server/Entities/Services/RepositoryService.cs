using System;
using System.Collections.Generic;
using Ateliware.Server.Entities.Interfaces.Repositories;
using Ateliware.Server.Entities.Interfaces.Services;
using Ateliware.Shared.ViewModel;

namespace Ateliware.Server.Entities.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly IRepositoryRepository _repositoryRepository;
        private Repository _repository;
        public RepositoryService(IRepositoryRepository repositoryRepository)
        {
            _repositoryRepository = repositoryRepository;
            Errors = new Dictionary<string, string>();
        }
        public IEnumerable<Repository> GetAll()
        {
            return _repositoryRepository.GetAll();
        }

        public void Add(ItemViewModel model)
        {
            if (_repositoryRepository.FirstOrDefault(m => m.Id.Equals(model.Id)) != null)
            {
                Errors.Add("exist","já existe este repositorio");
            }
            _repository = new Repository(model.Id,model.Name, model.Url, model.Language);
        }

        public bool IsValid
        {
            get => Errors.Count.Equals(0);
            set => throw new NotImplementedException();
        }

        public Dictionary<string, string> Errors { get; set; }
        public void Save()
        {
            if (!IsValid)
            {
                throw new Exception("Não foi possivel salvar, há erros");
            }
            _repositoryRepository.Add(_repository);
            _repositoryRepository.SaveChanges();
        }

        public void Remove(int id)
        {
            _repositoryRepository.Remove(id);
            _repositoryRepository.SaveChanges();
        }
    }
}
