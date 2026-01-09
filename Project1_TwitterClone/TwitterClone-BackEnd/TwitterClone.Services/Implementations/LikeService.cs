using System.Linq;
using TwitterClone.DataAccess.Interfaces;
using TwitterClone.Domain.Entities;
using TwitterClone.Services.Interfaces;

public class LikeService : ILikeService
{
    private readonly ILikeRepository _likeRepo;
    private readonly IPostRepository _postRepo;
    private readonly IUserRepository _userRepo;

    public LikeService(ILikeRepository likeRepo, IPostRepository postRepo, IUserRepository userRepo)
    {
        _likeRepo = likeRepo;
        _postRepo = postRepo;
        _userRepo = userRepo;
    }

    public void ToggleLike(int userId, int postId)
    {
        var user = _userRepo.GetById(userId);
        if (user == null) throw new Exception($"User with ID {userId} not found.");

        var post = _postRepo.GetById(postId);
        if (post == null) throw new Exception($"Post with ID {postId} not found.");

        var existingLike = _likeRepo.GetByUserAndPost(userId, postId);
        if (existingLike != null)
        {

            _likeRepo.Remove(existingLike);
        }
        else
        {

            var like = new Like
            {
                UserId = userId,
                PostId = postId
            };
            _likeRepo.Add(like);
        }
    }

    public int GetLikesCount(int postId)
    {
        return _likeRepo.GetByPostId(postId).Count;
    }
}
