using JwtDemo.Models;

namespace JwtDemo.Services;

public interface IUserService
{
    public User Get(UserLogin userLogin);
}