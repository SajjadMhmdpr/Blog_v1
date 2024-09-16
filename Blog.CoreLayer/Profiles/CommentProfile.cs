using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.CoreLayer.Dtos.Category;
using Blog.CoreLayer.Dtos.Comment;
using Blog.DataLayer.Entities;

namespace Blog.CoreLayer.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<PostComment, CommentDto>()
                .ForMember(dest =>
                    dest.UserFullName,
                opt =>
                    opt.MapFrom(src => src.User.FullName)); ;//< source , destination >
            CreateMap<CreateCommentDto, PostComment>();
        }
        
    }
}
