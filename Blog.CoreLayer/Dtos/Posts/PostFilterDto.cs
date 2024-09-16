using Blog.CoreLayer.Services.Posts;
using Blog.CoreLayer.Utilities;

namespace Blog.CoreLayer.Dtos.Posts;

public class PostFilterDto : BasePagination
{
    public List<Blog.CoreLayer.Dtos.Posts.PostDto> Posts { get; set; }
    public PostFilterParams FilterParams { get; set; }
}