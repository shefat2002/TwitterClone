using TwitterClone.Application.Dtos.User;
using TwitterClone.Application.Interfaces;
using TwitterClone.Domain.Entities;

namespace TwitterClone.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService (IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public UserDto? CreateUserDto(CeateUserDto createUserDto)
    {
        if(string.IsNullOrWhiteSpace(createUserDto.FirstName) || string.IsNullOrWhiteSpace(createUserDto.LastName) || string.IsNullOrWhiteSpace(createUserDto.Email))
        {
            return null;
        }
        var existingUser = _userRepository.GetUserByEmail(createUserDto.Email);
        if (existingUser != null)
        {
            return null;
        }
        var createdUser = _userRepository.AddUser(new User
        {
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
            Email = createUserDto.Email
        });
        return new UserDto
        {
            Id = createdUser.Id,
            FirstName = createdUser.FirstName,
            LastName = createdUser.LastName,
            Email = createdUser.Email
        };
    }

    public UserDto GetUserById(Guid id)
    {
        var user = _userRepository.GetUserById(id);
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }

    public List<UserDto> GetUsers()
    {
        var users = _userRepository.GetUsers();
        return users.Select(user => new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        }).ToList();
    }

    public UserDto UpdateUser(Guid id, UpdateUserDto updateUserDto)
    {
        var user = _userRepository.GetUserById(id);
        if (user == null)
        {
            return null;
        }
        
        user.FirstName = updateUserDto.FirstName;
        user.LastName = updateUserDto.LastName;
        
        var updatedUser = _userRepository.UpdateUser(user);
        
        return new UserDto
        {
            Id = updatedUser.Id,
            FirstName = updatedUser.FirstName,
            LastName = updatedUser.LastName,
            Email = updatedUser.Email
        };
    }

    public bool DeleteUser(Guid id)
    {
        var user = _userRepository.GetUserById(id);
        if (user == null)
        {
            return false;
        }

        var isDeleted = _userRepository.DeleteUser(user);

        return true;
    }
}