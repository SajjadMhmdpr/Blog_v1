using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.CoreLayer.Dtos.Comment;
using Blog.CoreLayer.Utilities;

namespace Blog.CoreLayer.Services.Comments
{
    public interface ICommentService
    {
        OperationResult CreateComment(CreateCommentDto dto);
        List<CommentDto> GetComments(int postId);

    }
}
