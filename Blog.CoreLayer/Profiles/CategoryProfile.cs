using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.CoreLayer.Dtos.Category;
using Blog.CoreLayer.Models.Category;
using Blog.DataLayer.Entities;

namespace Blog.CoreLayer.Profiles
{
    internal class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category,CategoryDto>();//< source , destination >
            CreateMap<CreateCategoryDto,Category>();
            CreateMap<EditCategoryDto,Category>();

            CreateMap<CreateCategoryViewModel,CreateCategoryDto>();
            CreateMap<EditCategoryViewModel,EditCategoryDto>();
            CreateMap<CategoryDto, EditCategoryViewModel>();

        }
    }
}
