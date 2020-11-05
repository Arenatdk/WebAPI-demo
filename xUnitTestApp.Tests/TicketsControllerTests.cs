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
    public abstract class TicketsControllerTests
    {
        #region Seeding
        protected TicketsControllerTests(DbContextOptions<WebApiContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        protected DbContextOptions<WebApiContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new WebApiContext(ContextOptions))
            {
               // context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();

                User usr = new User { Name = "Alice", Age = 31 };
                context.Users.Add(usr);

                context.Tickets.Add(new Ticket { Name = "Tom", User = usr });
                context.Tickets.Add(new Ticket { Name = "Alice", User = usr });
                context.SaveChanges();

                
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
                Ticket t = new Ticket() { Name = "test write", User = usr };
                context.Tickets.Add(t);
                context.SaveChanges();

                var controller = new TicketsController(context);

                var item = controller.Get(t.Id).Result.Value;

                Assert.Equal(t, item);
            }
        }

        [Fact]
        public void Can_get_all_itemS()
        {
            using (var context = new WebApiContext(ContextOptions))
            {
                User usr = new User() { Name = "Test User" };
                Ticket t = new Ticket() { Name = "test write", User = usr };
                context.Tickets.Add(t);
                context.SaveChanges();
                int count = context.Tickets.Count();
                var controller = new TicketsController(context);

                var item = controller.Get().Result.Value;

                Assert.Equal(count, item.Count());
            }
        }

        [Fact]
        public void Cant_get_item_if_not_exist()
        {
            using (var context = new WebApiContext(ContextOptions))
            {          
                var controller = new TicketsController(context);
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
                Ticket t = new Ticket() { Name = "test write", User = usr };
                context.Tickets.Add(t);
                context.SaveChanges();
                Ticket newticket = context.Tickets.Find(t.Id);

                var controller = new TicketsController(context);
                t.Name = "new value";
                var item = controller.Put(t);

                Assert.IsType<OkObjectResult>(item.Result);
                Assert.Equal(newticket, t);
                Assert.Equal("new value", newticket.Name);
            }
        }


        [Fact]
        public void Cant_put_null()
        {
            using (var context = new WebApiContext(ContextOptions))
            {
                

                var controller = new TicketsController(context);
                
                var item = controller.Put(null);

                Assert.IsType<BadRequestResult>(item.Result);
                
            }
        }

        [Fact]
        public void Cant_put_not_exist_item()
        {
            using (var context = new WebApiContext(ContextOptions))
            {
                Ticket t = new Ticket() { Name = "not", Id = 999999 };

                var controller = new TicketsController(context);

                var item = controller.Put(t);

                Assert.IsType<NotFoundResult>(item.Result);

            }
        }

        [Fact]
        public void Can_post_item()
        {
            using (var context = new WebApiContext(ContextOptions))
            {
                User usr = new User() { Name = "Test User" };
                context.Users.Add(usr);
                context.SaveChanges();
                Ticket t = new Ticket() { Name = "test write", UserId = usr.Id };

               
                

                var controller = new TicketsController(context);
                //t.Name = "new value";
                var item = controller.Post(t).Result;
                Ticket newticket = context.Tickets.Find(t.Id);

                Assert.IsType<ActionResult<Ticket>>(item);
                Assert.Equal(t, newticket);
            }
        }


        [Fact]
        public void Cant_post_null()
        {
            using (var context = new WebApiContext(ContextOptions))
            {


                var controller = new TicketsController(context);

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
                Ticket t = new Ticket() { Name = "test write", User = usr };
                context.Tickets.Add(t);
                context.SaveChanges();
                int delete = t.Id;


                var controller = new TicketsController(context);

                var item = controller.Delete(delete);

                
                Ticket newticket = context.Tickets.FirstOrDefault(x => x.Id == delete);
                Assert.Null(newticket);
                Assert.IsType<ActionResult<Ticket>>(item.Result);
             

            }
        }


        [Fact]
        public void Cant_delete_null()
        {
            using (var context = new WebApiContext(ContextOptions))
            {


                var controller = new TicketsController(context);

                var item = controller.Put(null);

                Assert.IsType<BadRequestResult>(item.Result);

            }
        }

        [Fact]
        public void Cant_delete_not_exist_item()
        {
            using (var context = new WebApiContext(ContextOptions))
            {
                Ticket t = new Ticket() { Name = "not", Id = 999999 };

                var controller = new TicketsController(context);

                var item = controller.Delete(t.Id);

                Assert.IsType<NotFoundResult>(item.Result.Result);

            }
        }


    }
}
