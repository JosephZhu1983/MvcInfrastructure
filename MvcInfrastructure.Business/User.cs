using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcInfrastructure.Sample.Business
{
    public class User : IUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }
    }
}
