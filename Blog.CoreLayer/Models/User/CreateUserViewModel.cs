﻿using Blog.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreLayer.Models.User
{
    public class CreateUserViewModel
    {
        [Display(Name = "نام و فامیل")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string FullName { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string UserName { get; set; }
        [Display(Name = "رمز")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MinLength(6, ErrorMessage = "{0} باید بیشتر از 5 کاراکتر باشد")]
        //[DataType(DataType.Password)]
        //[RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }
        [Display(Name = "نقش")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public UserRole Role { get; set; }
    }
}
