using TwitterClone.DTOs.Posts;

namespace TwitterClone.Services.Interfaces
{
    public interface IPostService
    {
        void CreatePost(CreatePostDto dto, int userId);
        List<PostDto> GetFeed();
        PostDto GetPostById(int id);
        void Retweet(int postId, int userId);
    }
}
