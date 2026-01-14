using TwitterClone.Domain.Entities;
using TwitterClone.Domain.Entities;
using TwitterClone.DTOs.Posts;

namespace TwitterClone.Mappers
{
    public static class PostMapper
    {
        public static PostDto ToPostDto(this Post post)
        {
            return new PostDto
            {
                Id = post.Id,
                Content = post.Content,
                Username = post.User.Username,
                LikesCount = post.Likes?.Count ?? 0,
                RetweetId = post.RetweetPostId,
                CreatedAt = post.CreatedAt

            };
        }
    }
}

