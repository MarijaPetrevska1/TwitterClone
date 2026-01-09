using TwitterClone.Domain.Entities;
using TwitterClone.DTOs.Likes;

namespace TwitterClone.Mappers
{
    public static class LikeMapper
    {
        public static LikeDto ToLikeDto(this Like like)
        {
            return new LikeDto
            {
                UserId = like.UserId,
                PostId = like.PostId
            };
        }
    }
}

