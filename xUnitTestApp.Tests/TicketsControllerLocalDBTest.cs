using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Models;

namespace xUnitTestApp.Tests
{
    public class TicketsControllerLocalDBTest : TicketsControllerTests
    {
        public TicketsControllerLocalDBTest()
            : base(
                new DbContextOptionsBuilder<WebApiContext>()
                    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=/TicketTestSample;ConnectRetryCount=0")
                    .Options)
        {
        }
    }
}
