using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.AppExceptions;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;
using SocialMedia.Infrastructure.Validations.PostValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        /*Crear mi api*/
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var post = await _postRepository.GetPosts();
            //Convertir una ENtidad a una EntidadDto
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(post);

            return Ok(postsDto);
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPostById(int postId)
        {
            var post = await _postRepository.GetPostById(postId);
            var postDto = _mapper.Map<PostDto>(post);
            //new PostDto
            //{
            //    PostId = post.PostId,
            //    Date = post.Date,
            //    Description = post.Description,
            //    Image = post.Image,
            //    UserId = post.UserId,
            //};
            return Ok(postDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            //    new Post
            //{

            //    Date = postDto.Date,
            //    Description = postDto.Description,
            //    Image = postDto.Image,
            //    UserId = postDto.UserId,
            //};

            var validator = new PostValidation();
            var validationResult = validator.Validate(postDto);

            if (!validationResult.IsValid)
            {
                throw new AppException(
                    "Ha ocurrido un error",
                    validationResult.Errors.Select(validation => new ErrorMessage
                        {
                            Message = validation.ErrorMessage,
                            Code = validation.ErrorCode
                        }
                    )
                );

            }

            await _postRepository.InsertPost(post);
            return Ok(post);

           
            }

        [HttpPut]

        public async Task<IActionResult> UpdatePost(int postId,PostDto postDto )
        {
            var post = _mapper.Map<Post>(postDto);

            var validator = new PostValidation();
            var validationResult = validator.Validate(postDto);

            if (!validationResult.IsValid)
            {
                throw new AppException(
                    "Ha ocurrido un error",
                    validationResult.Errors.Select(validation => new ErrorMessage
                    {
                        Message = validation.ErrorMessage,
                        Code = validation.ErrorCode
                    }
                    )
                );

            }

            await _postRepository.InsertPost(post);
            return Ok(post);

        }
        [HttpDelete]

        public async Task<IActionResult> DeletePost(int postId)
        {

            await _postRepository.Remove(postId);
            return Ok(postId);

        }
    }
}
