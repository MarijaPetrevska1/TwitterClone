using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.DTOs.Posts;
using TwitterClone.Services.Interfaces;

namespace TwitterClone.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // CREATE POST
        [HttpPost]
        public IActionResult CreatePost([FromBody] CreatePostDto dto)
        {
            try
            {

                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                int userId = userIdClaim != null ? int.Parse(userIdClaim) : 1;

                _postService.CreatePost(dto, userId);

                return Ok(new { message = "Post created successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // GET FEED
        [HttpGet("feed")]
        [AllowAnonymous] 
        public IActionResult GetFeed()
        {
            var posts = _postService.GetFeed();
            return Ok(posts);
        }

        // RETWEET
        [HttpPost("{postId}/retweet")]
        public IActionResult Retweet(int postId)
        {
            try
            {
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                int userId = userIdClaim != null ? int.Parse(userIdClaim) : 1;

                _postService.Retweet(postId, userId);

                return Ok(new { message = "Retweet successful." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
