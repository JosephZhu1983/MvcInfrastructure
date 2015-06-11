using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcInfrastructure.Sample.Business
{
    public class TestService : ITestService
    {
        public string Hello()
        {
            return "OK" + this.GetHashCode();
        }

        public User GetUser()
        {
            return new User { FirstName = "Ye", LastName = "Zhu", Age = 28, Address = "上海" };
        }

        public User[] GetUsers()
        {
            return new User[] { new User { FirstName = "Ye", LastName = "Zhu", Age = 28, Address = "上海" },
                                new User { FirstName = "aa", LastName = "bb", Age = 28, Address = "dd" }};
        }

        public Product GetProduct()
        {
            return new Product { Name = "Computer", Price = 10000, DisCount = 1000 };
        }
    }
}
