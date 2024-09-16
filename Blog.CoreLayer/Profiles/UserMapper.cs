using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.CoreLayer.Dtos.Users;
using Blog.DataLayer.Entities;

namespace Blog.CoreLayer.Profiles
{
    public class UserMapper
    {
        public static UserDto Map(User user)
        {
            return new UserDto()
            {
                UserName = user.UserName,
                Password = user.Password,
                FullName = user.FullName,
                CreateDate = user.CreationDate,
                Role = user.Role,
                Id = user.Id
            };
        }
        public static EditUserDto Map(UserDto user)
        {
            return new EditUserDto()
            {
                FullName = user.FullName,
                Role = user.Role,
                UserId = user.Id
            };
        }
    }
}
