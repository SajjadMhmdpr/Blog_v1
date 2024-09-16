using Blog.CoreLayer.Dtos.Posts;
using Blog.CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreLayer.Services.Posts
{
    public interface IPostService
    {
        OperationResult CreatePost(CreatePostDto command);
        OperationResult EditPost(EditPostDto command);
        PostDto? GetPostById(int postId);
        PostDto? GetPostBySlug(string slug);
        List<PostDto> GetPosts();
        PostFilterDto GetPostsByFilter(PostFilterParams filterParams);
        List<PostDto> GetMustVisitedPosts(int num);
        List<PostDto> GetRelatedPosts(int categoryId);
        bool IsSlugExist(string slug);
        void PostVisited(int id);
    }
}
