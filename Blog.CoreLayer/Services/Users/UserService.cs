using System;
using System.Linq;
using Blog.CoreLayer.Dtos.Posts;
using Blog.CoreLayer.Dtos.Users;
using Blog.CoreLayer.Models.User;
using Blog.CoreLayer.Profiles;
using Blog.CoreLayer.Utilities;
using Blog.DataLayer.Context;
using Blog.DataLayer.Entities;

using Microsoft.EntityFrameworkCore;

namespace Blog.CoreLayer.Services.Users
{
    public class UserService : IUserService
    {
        private readonly BlogContext _context;

        public UserService(BlogContext context)
        {
            _context = context;
        }

        public OperationResult RegisterUser(UserRegisterDto registerDto)
        {
            var isUserNameExist = _context.Users.Any(u => u.UserName == registerDto.UserName);
            if (isUserNameExist)
                return OperationResult.Error("نام کاربری تکراری است");

            var passwordHash = registerDto.Password.EncodeToMd5();
            _context.Users.Add(new User()
            {
                FullName = registerDto.FullName,
                IsDelete = false,
                UserName = registerDto.UserName,
                Role = UserRole.User,
                CreationDate = DateTime.Now,
                Password = passwordHash
            });
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult CreateUser(CreateUserViewModel viewModel)
        {
            var isUserNameExist = _context.Users.Any(u => u.UserName == viewModel.UserName);
            if (isUserNameExist)
                return OperationResult.Error("نام کاربری تکراری است");

            var passwordHash = viewModel.Password.EncodeToMd5();
            _context.Users.Add(new User()
            {
                FullName = viewModel.FullName,
                IsDelete = false,
                UserName = viewModel.UserName,
                Role = viewModel.Role,
                CreationDate = DateTime.Now,
                Password = passwordHash
            });
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult LoginUser(UserLoginDto loginDto)
        {
            var passHash = loginDto.Password.EncodeToMd5();

            var isUserExist = _context.Users.Any(u => u.UserName == loginDto.UserName & u.Password== passHash);

            if (!isUserExist)
                return OperationResult.Error("حسابی با این مشخصات یافت نشد");

            return OperationResult.Success();

        }

        public OperationResult EditUser(EditUserDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == dto.UserId);
            if (user == null)
            {
                return OperationResult.NotFound();
            }

            user.FullName = dto.FullName;
            user.Role = dto.Role;

            _context.SaveChanges();

            return OperationResult.Success();

        }

        public UserDto LoginUser2(UserLoginDto loginDto)
        {
            var passHash = loginDto.Password.EncodeToMd5();

            var user = _context.Users.FirstOrDefault(u => u.UserName == loginDto.UserName & u.Password == passHash);

            if (user == null)
                return new UserDto();

            return UserMapper.Map(user);
        }
        public UserFilterDto GetUsersByFilter(UserFilterParams filterParams)
        {
            var result = _context.Users
                .OrderByDescending(d => d.CreationDate).AsQueryable();

            //if (filterParams.Role!=null)
            //    result = result.Where(r => r.Role == filterParams.Role );

            if (!string.IsNullOrWhiteSpace(filterParams.FullName))
                result = result.Where(r => r.FullName.Contains(filterParams.FullName));

            var skip = (filterParams.PageId - 1) * filterParams.Take;

            var model = new UserFilterDto()
            {
                Users = result.Skip(skip).Take(filterParams.Take)
                    .Select(user => UserMapper.Map(user)).ToList(),
                FilterParams = filterParams

            };

            model.GeneratePaging(result, filterParams.Take, filterParams.PageId);

            return model;
        }
        public List<UserDto> GetAllUsers()
        {
            return _context.Users.Select(u => UserMapper.Map(u)).ToList();
        }

        public UserDto GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id==id);

            if (user == null)
                return null;
            return UserMapper.Map(user);
        }
    }
}