﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.DataLayer.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDelete { get; set; }
    }
}