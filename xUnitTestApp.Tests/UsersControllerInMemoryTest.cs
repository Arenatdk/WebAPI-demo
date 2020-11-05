using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Models;

namespace xUnitTestApp.Tests
{
    public class UsersControllerInMemoryTest : UsersControllerTests
    {
        public UsersControllerInMemoryTest()
            : base(
                new DbContextOptionsBuilder<WebApiContext>()
                    .UseInMemoryDatabase(databaseName: "UserDB")
                .Options)
        {
        }
    }
}
