using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        /*Crear mi api*/
        private readonly IPostRepository _postRepository;
        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var post = await _postRepository.GetPosts();
            return Ok(post);
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPostById(int postId)
        {
            var post = await _postRepository.GetPostById(postId);
            return Ok(post);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Post post )
        {
            await _postRepository.InsertPost(post);
            return Ok(post);
        }
    }
}
