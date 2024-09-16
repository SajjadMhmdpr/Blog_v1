using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreLayer.Models.Post
{
    public class CreatePostViewModel
    {
        public int UserId { get; set; }
        [Display(Name = "انتخاب دسته بندی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public int CategoryId { get; set; }
        [Display(Name = "انتخاب دسته بندی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public int SubCategoryId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "slug")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Slug { get; set; }
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Description { get; set; }
        [Display(Name = "عکس")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public IFormFile ImageFile { get; set; }
        [Display(Name = "پست ویژه")]
        public bool IsSpecial { get; set; }


    }
}
