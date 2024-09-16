using Blog.CoreLayer.Dtos.Posts;
using Blog.CoreLayer.Services.Posts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Pages
{
    public class SearchResultModel : PageModel
    {
        private readonly IPostService _postService;

        public SearchResultModel(IPostService postService)
        {
            _postService = postService;
        }
        public PostFilterDto Filter { get; set; }

        public void OnGet(int pageId = 1, string categorySlug = null, string q = null)
        {
            Filter = _postService.GetPostsByFilter(new PostFilterParams()
            {
                CategorySlug = categorySlug,
                PageId = pageId,
                Take = 1,
                Title = q
            });
        }
        public IActionResult OnGetPagination(int pageId = 1, string categorySlug = null, string q = null)
        {
            Filter = _postService.GetPostsByFilter(new PostFilterParams()
            {
                CategorySlug = categorySlug,
                PageId = pageId,
                Take = 1,
                Title = q
            });
            return Partial("_SearchView",Filter);
        }
    }
}
