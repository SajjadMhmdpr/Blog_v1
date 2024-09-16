using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLayer.Entities
{
    public class Post : BaseEntity
    {

        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public bool IsSpecial { get; set; }
        public int Visit { get; set; }

        #region Relations

        public User User { get; set; }
        public Category Category { get; set; }
        public Category SubCategory { get; set; }
        public ICollection<PostComment> PostComments { get; set; }

        #endregion
    }
}
