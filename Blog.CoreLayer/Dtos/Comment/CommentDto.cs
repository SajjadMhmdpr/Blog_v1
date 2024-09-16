﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreLayer.Dtos.Comment
{
    public class CommentDto
    {
        public string  UserFullName { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
