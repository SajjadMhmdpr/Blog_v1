using Blog.CoreLayer.Dtos;
using Blog.CoreLayer.Dtos.Posts;
using Blog.CoreLayer.Services.MainPage;
using Blog.CoreLayer.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog_v1.Pages
{
    
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPostService _postService;
        private readonly IMainPageService _mainPageService;

        public IndexModel(ILogger<IndexModel> logger, IPostService postService,
            IMainPageService mainPageService)
        {
            _logger = logger;
            _postService = postService;
            _mainPageService = mainPageService;
        }

        public MainPageDto PageDto { get; set; }
        public void OnGet()
        {
            //throw new Exception();
            PageDto = _mainPageService.GetData();
        }

        public void OnPost()
        {

        }

        public IActionResult OnGetPopularPosts()
        {
            return Partial("_PopularPosts",_postService.GetMustVisitedPosts(4));
        }
        public IActionResult OnGetLatestPosts(string categorySlug="")
        {
            var postFilter = _postService.GetPostsByFilter(new PostFilterParams()
            {
                PageId = 1,
                CategorySlug = categorySlug,
                Take = 4
            });
            return Partial("_LatestPosts", postFilter.Posts);
        }
    }
}