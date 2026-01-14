using TwitterClone.DTOs.Comments;
using TwitterClone.DTOs.Posts;

namespace TwitterClone.Services.Interfaces
{
    public interface IPostService
    {

        List<CommentDto> GetComments(int postId);
        void AddComment(int postId, int userId, string content);
        void CreatePost(CreatePostDto dto, int userId);
        List<PostDto> GetFeed(int? userId);
        PostDto GetPostById(int id);
        void Retweet(int postId, int userId);

        void ToggleLike(int postId, int userId);
    }
}
