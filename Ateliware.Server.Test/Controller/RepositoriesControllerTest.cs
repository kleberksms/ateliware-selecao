
using System.Collections.Generic;
using System.Linq;
using Ateliware.Server.Controllers;
using Ateliware.Server.Entities.Services;
using Ateliware.Server.Repository;
using Ateliware.Server.Repository.Context;
using Ateliware.Shared.Enums;
using Ateliware.Shared.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ateliware.Server.Test.Controller
{
    public class RepositoriesControllerTest
    {
        public RepositoriesControllerTest()
        {
            InitContext();
        }

        private AteliwareContext _ateliwareContext;

        private void InitContext()
        {
            var builder = new DbContextOptionsBuilder<AteliwareContext>()
                .UseInMemoryDatabase();

            var context = new AteliwareContext(builder.Options);
            var repositories = Enumerable.Range(1, 8)
                .Select(i => new Entities.Repository(i,$"Foo {i}",$"http{i}",(Language)i));
            context.Repositories.AddRange(repositories);
            var changed = context.SaveChanges();
            _ateliwareContext = context;
        }

        [Fact]
        public void TestIfCallbackOnGetMethodIsAList()
        {
            const string expected = "Foo 1";
            // Atc
            var controller = new RepositoriesController(new RepositoryService(new RepositoryRepository(_ateliwareContext)));
            var actionResult = controller.Get();

            //Assert
            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as IEnumerable<ItemViewModel>;
            Assert.NotNull(model);

            var actual = model.First().Name;
            Assert.Equal(expected, actual);

            Assert.IsAssignableFrom<IEnumerable<ItemViewModel>>(model);
        }

        [Fact]
        public void ErrorOnAddSameObject()
        {
            
            var controller = new RepositoriesController(new RepositoryService(new RepositoryRepository(_ateliwareContext)));
            var errors = new ModelStateDictionary();
            errors.AddModelError("exist", "já existe este repositorio");
            // Atc
            var item = new ItemViewModel{Language = Language.Clojure, Id = 99, Name = "Foo", Url = "Bar"};
            var actionOkResult = controller.Post(item);
            var actionBadResult = controller.Post(item);

            //Assert
            var okObjectResult = actionOkResult as OkObjectResult;
            Assert.Null(okObjectResult);

            var badRequestObjectResult = actionBadResult as BadRequestObjectResult;
            Assert.NotNull(badRequestObjectResult);

            var model = badRequestObjectResult.Value as ModelStateDictionary;
        }

    }
}
