using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Models;

namespace xUnitTestApp.Tests
{
    public class UsersControllerLocalDBTest : UsersControllerTests
    {
        public UsersControllerLocalDBTest()
            : base(
                new DbContextOptionsBuilder<WebApiContext>()
                    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=UserTestSample;ConnectRetryCount=0")
                    .Options)
        {
        }
    }
}
