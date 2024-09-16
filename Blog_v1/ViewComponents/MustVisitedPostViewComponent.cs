using Blog.CoreLayer.Dtos.Posts;
using Blog.CoreLayer.Services.Posts;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.ViewComponents
{
    public class MustVisitedPostViewComponent : ViewComponent
    {
        private readonly IPostService _postService;

        public MustVisitedPostViewComponent(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View("MustVisitedPost",
                _postService.GetMustVisitedPosts(4)));
        }
    }
}
