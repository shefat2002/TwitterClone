using TwitterClone.Application.Dtos.User;

namespace TwitterClone.Application.Interfaces;

public interface IUserService
{
    UserDto CreateUserDto(CeateUserDto createUserDto);
    UserDto GetUserById(Guid id);
    List<UserDto> GetUsers();
    UserDto UpdateUser(Guid id, UpdateUserDto updateUserDto);
    bool DeleteUser(Guid id);
}