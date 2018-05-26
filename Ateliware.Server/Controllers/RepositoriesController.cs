using System.Linq;
using Ateliware.Server.Entities.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Ateliware.Shared.ViewModel;

namespace Ateliware.Server.Controllers
{
    [Route("api/[controller]")]
    public class RepositoriesController : Controller
    {
        private readonly IRepositoryService _repositoryService;

        public RepositoriesController(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _repositoryService.GetAll().Select(c => new ItemViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Url = c.Url,
                Language = c.Language
            }).ToList();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody]ItemViewModel model)
        {

            _repositoryService.Add(model);
            if (!_repositoryService.IsValid)
            {
                foreach (var keyValuePair in _repositoryService.Errors)
                {
                    ModelState.AddModelError(keyValuePair.Key,keyValuePair.Value);
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repositoryService.Save();
            return Ok();
        }

        [HttpDelete, Route("{id}")]
        public IActionResult Delete(int id)
        {
            _repositoryService.Remove(id);
            return Ok();
        }
    }
}
