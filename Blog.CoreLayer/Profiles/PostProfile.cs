
using Blog.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.CoreLayer.Dtos.Category;
using Blog.CoreLayer.Dtos.Posts;
using Blog.CoreLayer.Models.Post;
using Blog.CoreLayer.Services.Categories;
using Blog.CoreLayer.Services.Posts;

namespace Blog.CoreLayer.Profiles
{
    public class PostProfile : Profile

    {

        public PostProfile()
        {

            CreateMap<CreatePostDto, Post>();//< source , destination >
            CreateMap<EditPostDto, Post>().ForMember(dest =>
                    dest.Id,
                opt =>
                    opt.MapFrom(src => src.PostId)); ;
            //CreateMap<Post, PostDto>();
            CreateMap<Post, PostDto>().ForMember(dest =>
                    dest.PostId,
                opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest =>
                        dest.UserFullName,
                    opt =>
                        opt.MapFrom(src => src.User.FullName));

            CreateMap<CreatePostViewModel, CreatePostDto>();
            CreateMap<PostDto, EditPostViewModel>();
            CreateMap<EditPostViewModel,EditPostDto>();

            //.ForMember(dest =>
            //        dest.Category,
            //    opt =>
            //        opt.MapFrom(src =>src .Map<CategoryDto>(src.Category)));


        }
    }
}
