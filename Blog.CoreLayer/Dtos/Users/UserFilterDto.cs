using Blog.CoreLayer.Dtos.Posts;
using Blog.DataLayer.Entities;
using Blog.CoreLayer.Utilities;

namespace Blog.CoreLayer.Dtos.Users
{
    public class UserFilterDto : BasePagination
    {
        public List<UserDto> Users { get; set; }
        public UserFilterParams FilterParams { get; set; }
    }
    public class UserFilterParams
    {
        public string FullName { get; set; }
        public UserRole Role { get; set; }
        public int PageId { get; set; }
        public int Take { get; set; }
    }
}
