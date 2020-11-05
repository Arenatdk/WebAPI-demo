using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models;
using Xunit;

namespace xUnitTestApp.Tests
{
    public abstract class UsersControllerTests
    {
        #region Seeding
        protected UsersControllerTests(DbContextOptions<WebApiContext> contextOptions)
        {
            ContextOptions = contextOptions;

            //Seed();
        }

        protected DbContextOptions<WebApiContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new WebApiContext(ContextOptions))
            {
               // context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();

                //User usr = new User { Name = "Alice", Age = 31 };
                //context.Users.Add(usr);

                //context.Tickets.Add(new Ticket { Name = "Tom", User = usr });
                //context.Tickets.Add(new Ticket { Name = "Alice", User = usr });
                //context.SaveChanges();

                
            }
        }
        #endregion

        [Fact]
        public void Can_read_write_db()
        {
            var context = new WebApiContext(ContextOptions);
            {
                User usr = new User() { Name = "Test User" };
                Ticket t = new Ticket() { Name = "test write" ,User=usr};
                context.Tickets.Add(t);
                context.SaveChanges();
                Ticket resultTicket = context.Tickets.Find(t.Id);
                Assert.Equal(resultTicket,t);
            }

        }


        [Fact]
        public void Can_get_item()
        {
            using (var context = new WebApiContext(ContextOptions))
            {
                User usr = new User() { Name = "Test User" };
                context.Users.Add(usr);
                context.SaveChanges();

                var controller = new UsersController(context);

                var item = controller.Get(usr.Id).Result;

                Assert.Equal(usr, item.Value);
            }
        }

        [Fact]
        public void Can_get_all_itemS()
        {
            using (var context = new WebApiContext(ContextOptions))
            {
                User usr = new User() { Name = "Test User" };
                context.Users.Add(usr);
                context.SaveChanges();
                int count = context.Users.Count();
                var controller = new UsersController(context);

                var item = controller.Get().Result.Value;

                Assert.Equal(count, item.Count());
            }
        }

        [Fact]
        public void Cant_get_item_if_not_exist()
        {
            using (var context = new WebApiContext(ContextOptions))
            {          
                var controller = new UsersController(context);
                var item = controller.Get(9999999).Result.Value;
                Assert.Null(item);
            }
        }


        [Fact]
        public void Can_put_item()
        {
            using (var context = new WebApiContext(ContextOptions))
            {
                User usr = new User() { Name = "Test User" };
                context.Users.Add(usr);
                context.SaveChanges();
                User newuser = context.Users.Find(usr.Id);

                var controller = new UsersController(context);
                usr.Name = "new value";
                var item = controller.Put(usr);

                Assert.IsType<OkObjectResult>(item.Result.Result);
                Assert.Equal(newuser, usr);
                Assert.Equal("new value", newuser.Name);
            }
        }


        [Fact]
        public void Cant_put_null()
        {
            using (var context = new WebApiContext(ContextOptions))
            {
                

                var controller = new UsersController(context);
                
                var item = controller.Put(null);

                Assert.IsType<BadRequestResult>(item.Result.Result);
                
            }
        }

        [Fact]
        public void Cant_put_not_exist_item()
        {
            using (var context = new WebApiContext(ContextOptions))
            {
                User u = new User() { Name = "not", Id = 999999 };

                var controller = new UsersController(context);

                var item = controller.Put(u);

                Assert.IsType<NotFoundResult>(item.Result.Result);

            }
        }

        [Fact]
        public void Can_post_item()
        {
            using (var context = new WebApiContext(ContextOptions))
            {
                User usr = new User() { Name = "Test User",Age=123 };
                var controller = new UsersController(context);
                //t.Name = "new value";
                var item = controller.Post(usr);
                

                Assert.IsType<ActionResult<User>>(item.Result);
                User newusr = context.Users.Find(usr.Id);
                Assert.Equal(usr, newusr);
            }
        }


        [Fact]
        public void Cant_post_null()
        {
            using (var context = new WebApiContext(ContextOptions))
            {


                var controller = new UsersController(context);

                var item = controller.Post(null);

                Assert.IsType<BadRequestResult>(item.Result.Result);

            }
        }



        [Fact]
        public void Can_delete_item()
        {
            using (var context = new WebApiContext(ContextOptions))
            {
                User usr = new User() { Name = "Test User" };
                context.Users.Add(usr);    
                context.SaveChanges();
                int delete = usr.Id;


                var controller = new UsersController(context);

                var item = controller.Delete(delete);


                
                Assert.IsType<ActionResult<User>>(item.Result);
                User newusr = context.Users.FirstOrDefault(x => x.Id == delete);
                Assert.Null(newusr);

            }
        }


        [Fact]
        public void Cant_delete_null()
        {
            using (var context = new WebApiContext(ContextOptions))
            {


                var controller = new UsersController(context);

                var item = controller.Put(null);

                Assert.IsType<BadRequestResult>(item.Result.Result);

            }
        }

        [Fact]
        public void Cant_delete_not_exist_item()
        {
            using (var context = new WebApiContext(ContextOptions))
            {
                User newusr = new User() { Name = "not", Id = 999999 };

                var controller = new UsersController(context);

                var item = controller.Delete(newusr.Id);

                Assert.IsType<NotFoundResult>(item.Result.Result);

            }
        }


    }
}
