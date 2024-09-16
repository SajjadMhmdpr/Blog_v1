using Blog.CoreLayer.Dtos.Posts;
using Blog.CoreLayer.Dtos.Users;
using Blog.CoreLayer.Models.User;
using Blog.CoreLayer.Utilities;


namespace Blog.CoreLayer.Services.Users
{
    public interface IUserService
    {
        OperationResult RegisterUser(UserRegisterDto registerDto);
        OperationResult CreateUser(CreateUserViewModel viewModel);
        OperationResult LoginUser(UserLoginDto registerDto);
        OperationResult EditUser(EditUserDto dto);
        UserDto LoginUser2(UserLoginDto registerDto);
        List<UserDto> GetAllUsers();
        UserDto GetUserById(int id);
        UserFilterDto GetUsersByFilter(UserFilterParams filterParams);
    }
}