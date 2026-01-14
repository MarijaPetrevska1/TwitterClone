using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Services.Interfaces;

namespace TwitterClone.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class LikesController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikesController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost("{postId}/toggle")]
        public IActionResult ToggleLike(int postId)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Unauthorized();

            int userId = int.Parse(userIdClaim);

            _likeService.ToggleLike(userId, postId);

            return Ok("Toggled like successfully.");
        }

        [HttpGet("{postId}/count")]
        public IActionResult GetLikesCount(int postId)
        {
            var count = _likeService.GetLikesCount(postId);
            return Ok(count);
        }
    }
}
