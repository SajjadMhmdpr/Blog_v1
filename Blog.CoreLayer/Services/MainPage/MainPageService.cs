using Blog.CoreLayer.Dtos;
using Blog.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.CoreLayer.Dtos.Posts;
using Microsoft.EntityFrameworkCore;

namespace Blog.CoreLayer.Services.MainPage
{
    public class MainPageService : IMainPageService
    {
        private readonly BlogContext _context;
        private readonly IMapper _mapper;
        public MainPageService(BlogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public MainPageDto GetData()
        {
            var categories = _context.Categories
                .OrderByDescending(d => d.Id)
                .Include(c => c.Posts)
                .Include(c => c.SubPosts)
                .Select(category => new MainPageCategoryDto()
                {
                    Title = category.Title,
                    Slug = category.Slug,
                    PostChild = category.Posts.Count + category.SubPosts.Count,
                    IsMainCategory = (category.ParentId == null)
                }).ToList();

            var specialPosts = _context.Posts
                .OrderByDescending(d => d.Visit)
                .Include(c => c.Category)
                .Include(c => c.SubCategory)
                .Include(c => c.User)
                .Where(r => r.IsSpecial).Take(4)
                .Select(post => _mapper.Map<PostDto>(post)).ToList();

            var latestPost = _context.Posts
                .Include(c => c.Category)
                .Include(c => c.SubCategory)
                .Include(c => c.User)
                .OrderByDescending(d => d.CreationDate)
                .Take(4)
                .Select(post => _mapper.Map<PostDto>(post)).ToList();

            return new MainPageDto()
            {
                LatestPosts = latestPost,
                Categories = categories,
                SpecialPosts = specialPosts
            };
        }
    }
}
