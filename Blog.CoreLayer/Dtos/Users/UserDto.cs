using Blog.DataLayer.Entities;

namespace Blog.CoreLayer.Dtos.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public UserRole Role { get; set; }
    }
}