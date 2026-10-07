using TwitterClone.Application.Dtos.User;
using TwitterClone.Domain.Entities;

namespace TwitterClone.Application.Interfaces;

public interface IUserRepository
{
    User AddUser(User user);
    User GetUserById(Guid id);
    User GetUserByEmail(string email);
    List<User> GetUsers();
    User UpdateUser(User user);
    bool DeleteUser(User id);
}