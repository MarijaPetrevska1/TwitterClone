using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.DTOs.Comments;
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
        //[HttpGet("feed")]
        //[AllowAnonymous] 
        //public IActionResult GetFeed()
        //{
        //    var posts = _postService.GetFeed();
        //    return Ok(posts);
        //}

        [HttpGet("feed")]
        public IActionResult GetFeed()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            int? userId = userIdClaim != null ? int.Parse(userIdClaim) : (int?)null;

            var posts = _postService.GetFeed(userId);
            return Ok(posts);
        }

        [HttpPost("{postId}/like")]
        public IActionResult ToggleLike(int postId)
        {
            try
            {
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                int userId = userIdClaim != null ? int.Parse(userIdClaim) : 1;

                _postService.ToggleLike(postId, userId);

                return Ok(new { message = "Like toggled successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
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

        [HttpPost("{postId}/comment")]
        public IActionResult AddComment(int postId, [FromBody] AddCommentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Unauthorized();

            int userId = int.Parse(userIdClaim);

            _postService.AddComment(postId, userId, dto.Content);

            return Ok(new
            {
                content = dto.Content,
                username = User.Identity?.Name
            });
        }

        [HttpGet("{postId}/comments")]
        [AllowAnonymous]
        public IActionResult GetComments(int postId)
        {
            try
            {
                var comments = _postService.GetComments(postId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }



    }
}
