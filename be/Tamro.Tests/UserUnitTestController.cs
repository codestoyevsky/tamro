using System;
using System.Configuration;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Tamro.Common.Services;
using Tamro.Entities;
using Tamro.Web.ApiModels;
using Tamro.Web.Controllers;
using Assert = Xunit.Assert;

namespace Tamro.Tests
{

    [TestFixture]
    public class UserUnitTestController
    {
        private readonly UserService userService;
        private readonly Mapper mapper;
        private readonly UserController controller;
        public static DbContextOptions<TamroDBContext> dbContextOptions { get; }
        //public static string connectionString = "Data Source=C:\\Sqlite\\tamro.db";

        static UserUnitTestController()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            
            dbContextOptions = new DbContextOptionsBuilder<TamroDBContext>()
                .UseSqlite(configuration.GetConnectionString("Default"))
                .Options;
        }

        public UserUnitTestController()
        {
            var context = new TamroDBContext(dbContextOptions);
            userService = new UserService(context);

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            mapper = new Mapper(configuration);
            controller = new UserController(userService, mapper);
        }

        [Test]
        public async Task Task_Get_By_Email_Existing_User()
        {
            var result = await controller.GetByEmail("tomas.medonas@tamro.com");
            Assert.IsType<OkObjectResult>(result);
        }

        [Test]
        public async Task Task_Get_By_Email_Not_Existing_User()
        {
            var result = await controller.GetByEmail("tamro@avidatech.eu");
            Assert.IsType<NotFoundResult>(result);
        }

        [Test]
        public async Task Task_Create_Valid_New_User()
        {
            var user = controller.GetByEmail("bill.gates@hotmail.com").Result as OkObjectResult;

            if (user != null)
            {
                var id = (user.Value as UserModel).Id;
                await controller.Delete(id);
            }

            var result = await controller.Create(new UserModel()
            {
                FirstName = "Bill",
                LastName = "Gates",
                Email = "bill.gates@hotmail.com",
                PhoneNumber = "58671075"
            });
            Assert.IsType<OkObjectResult>(result);

        }

        [Test]
        public async Task Task_Update_Not_Existing_User()
        {
            var result = await controller.Create(new UserModel() { Id = 99999, LastName = "Gates", Email = "bill.gates@hotmail.com", PhoneNumber = "58671075" });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task Task_Delete_Valid_Existing_User()
        {

            var user =  controller.GetByEmail("bill.gates@hotmail.com").Result as OkObjectResult;

            if (user == null)
            {
                await controller.Create(new UserModel()
                {
                    FirstName = "Bill",
                    LastName = "Gates",
                    Email = "bill.gates@hotmail.com",
                    PhoneNumber = "58671075"
                });
            }

            var id = (user.Value as UserModel).Id;
            var result = await controller.Delete(id);
            Assert.IsType<OkResult>(result);
        }

        [Test]
        public async Task Task_Delete_Not_Existing_User()
        {
            var result = await controller.Delete(99999);
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}