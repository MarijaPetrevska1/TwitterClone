using System;
using System.Collections.Generic;
using System.Linq;
using TwitterClone.DataAccess.Interfaces;
using TwitterClone.Domain.Entities;
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

    public List<PostDto> GetFeed()
    {
        var posts = _postRepo.GetAll();
        return posts.Select(p => p.ToPostDto()).ToList();
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
}
