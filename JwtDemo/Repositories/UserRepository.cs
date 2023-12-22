using JwtDemo.Models;

namespace JwtDemo.Repositories;

public class UserRepository
{
    public static List<User> Users = new List<User>()
    {
        new User()
        {
            Username = "admin",
            EmailAddress = "admin@admin.com",
            GivenName = "admin",
            Surname = "admin",
            Password = "admin@123",
            Role = "Administrator"
        },
        new User()
        {
            Username = "seller",
            EmailAddress = "seller",
            GivenName = "seller",
            Surname = "seller",
            Password = "seller@123",
            Role = "Standard"
        }
        
    };
}