using Microsoft.AspNetCore.Mvc;
using SocialMedia.Infrastructure.Repositories;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        /*Crear mi api*/

        [HttpGet]
        public IActionResult GetPosts()
        {
            var post = new PostRepository().GetPosts();
            return Ok(post);
        }
    }
}
