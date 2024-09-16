using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLayer.Entities
{
    public class PostComment: BaseEntity
    {
     
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }

 

        #region Relations

        public Post Post { get; set; }
        public User User { get; set; }

        #endregion
    }
}
