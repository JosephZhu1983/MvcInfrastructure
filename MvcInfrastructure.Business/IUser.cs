using System;

namespace MvcInfrastructure.Sample.Business
{
    public interface IUser
    {
        string Address { get; set; }
        int Age { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
