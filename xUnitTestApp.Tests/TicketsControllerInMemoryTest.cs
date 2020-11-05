using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Models;

namespace xUnitTestApp.Tests
{
    public class TicketsControllerInMemoryTest : TicketsControllerTests
    {
        public TicketsControllerInMemoryTest()
            : base(
                new DbContextOptionsBuilder<WebApiContext>()
                    .UseInMemoryDatabase(databaseName: "TicketDB")
                .Options)
        {
        }
    }
}
