using AutoMapper;
using Blog.CoreLayer.Dtos.Comment;
using Blog.CoreLayer.Utilities;
using Blog.DataLayer.Context;
using Blog.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.CoreLayer.Services.Comments;

public class CommentService : ICommentService
{
    private readonly BlogContext _blogContext;
    private readonly IMapper _mapper;


    public CommentService(BlogContext blogContext, IMapper mapper)
    {
        _blogContext = blogContext;
        _mapper = mapper;
    }

    public OperationResult CreateComment(CreateCommentDto dto)
    {
        var comment = _mapper.Map<PostComment>(dto);

        comment.CreationDate = DateTime.Now;
        comment.IsDelete = false;

        _blogContext.PostComments.Add(comment);
        _blogContext.SaveChanges();

        return OperationResult.Success();
    }

    public List<CommentDto> GetComments(int postId)
    {
        return _blogContext.PostComments
            .Include(c => c.User)
            .Where(c=>c.PostId== postId)
            .Select(c => _mapper.Map<CommentDto>(c))
            .ToList();
    }
}