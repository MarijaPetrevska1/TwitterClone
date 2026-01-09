using System.Collections.Generic;

namespace TwitterClone.Services.Interfaces
{
    public interface ILikeService
    {
        void ToggleLike(int userId, int postId);
        int GetLikesCount(int postId);
    }
}

