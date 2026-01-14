using System;
using System.Collections.Generic;
using System.Linq;
using TwitterClone.DataAccess.Implementations;
using TwitterClone.DataAccess.Interfaces;
using TwitterClone.Domain.Entities;
using TwitterClone.DTOs.Comments;
using TwitterClone.DTOs.Posts;
using TwitterClone.Mappers;
using TwitterClone.Services.Interfaces;
using TwitterClone.Shared.CustomExceptions;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepo;
    private readonly IUserRepository _userRepo;

    public PostService(IPostRepository postRepo, IUserRepository userRepo)
    {
        _postRepo = postRepo;
        _userRepo = userRepo;
    }

    public void CreatePost(CreatePostDto dto, int userId)
    {
        var user = _userRepo.GetById(userId);
        if (user == null)
            throw new UserNotFoundException(userId);

        var post = new Post
        {
            Content = dto.Content,
            UserId = userId,
            RetweetPostId = dto.RetweetPostId,
            CreatedAt = DateTime.UtcNow
        };

        _postRepo.Add(post);
    }

    public List<PostDto> GetFeed(int? currentUserId = null)
    {
        var posts = _postRepo.GetAll();
        return posts.Select(p => new PostDto
        {
            Id = p.Id,
            Content = p.Content,
            Username = p.User.Username,
            LikesCount = p.Likes.Count,
            RetweetId = p.RetweetPostId,
            CreatedAt = p.CreatedAt,
            UserLiked = currentUserId.HasValue && p.Likes.Any(l => l.UserId == currentUserId.Value)
        }).ToList();
    }
    public PostDto GetPostById(int id)
    {
        var post = _postRepo.GetById(id);
        if (post == null)
            throw new PostNotFoundException(id);

        return post.ToPostDto();
    }

    public void Retweet(int postId, int userId)
    {
        var originalPost = _postRepo.GetById(postId);
        if (originalPost == null)
            throw new PostNotFoundException(postId);

        var user = _userRepo.GetById(userId);
        if (user == null)
            throw new UserNotFoundException(userId);

        var retweet = new Post
        {
            Content = originalPost.Content,
            UserId = userId,
            RetweetPostId = originalPost.Id,
            CreatedAt = DateTime.UtcNow
        };

        _postRepo.Add(retweet);
    }

    public void ToggleLike(int postId, int userId)
    {
        var post = _postRepo.GetById(postId);
        if (post == null) throw new Exception("Post not found");

        var existingLike = post.Likes.FirstOrDefault(l => l.UserId == userId);

        if (existingLike != null)
        {
            // Remove like
            post.Likes.Remove(existingLike);
        }
        else
        {
            // Add like
            post.Likes.Add(new Like { UserId = userId, PostId = postId });
        }

        _postRepo.Update(post);
    }

    public List<CommentDto> GetComments(int postId)
    {
        var post = _postRepo.GetById(postId);
        if (post == null) throw new Exception("Post not found");

        return post.Comments
                   .Select(c => new CommentDto
                   {
                       Id = c.Id,
                       Content = c.Content,
                       Username = c.User.Username,
                       CreatedAt = c.CreatedAt
                   })
                   .ToList();
    }

    public void AddComment(int postId, int userId, string content)
    {
        var post = _postRepo.GetById(postId);
        if (post == null) throw new Exception("Post not found");

        var comment = new Comment
        {
            PostId = postId,
            UserId = userId,
            Content = content,
            CreatedAt = DateTime.UtcNow
        };

        _postRepo.AddComment(comment);
    }





}
