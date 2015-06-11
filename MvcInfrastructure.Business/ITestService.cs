using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcInfrastructure.Sample.Business
{
    public interface ITestService
    {
        string Hello();

        User GetUser();

        User[] GetUsers();

        Product GetProduct();
    }
}
