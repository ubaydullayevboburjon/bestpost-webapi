using BestPost.DataAccsess.Utils;
using BestPost.Service.Dtos.Posts;
using BestPost.Service.Interfaces.Posts;
using BestPost.Service.Validators.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestPost.WebApi.Controllers.Post;

[Route("api/posts")]
[ApiController]
[Authorize(Roles = "User")]
public class PostController : ControllerBase
{
     private readonly int maxPageSize = 8;
    IPostService  _postService;
    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpPost]

    public async Task<IActionResult> CreateAsync([FromForm] PostCreateDto postCreateDto)
    {
        var createValidator = new PostCreateValidator();
        var result = createValidator.Validate(postCreateDto);

        if (result.IsValid) return Ok(await  _postService.CreateAsync(postCreateDto));
        else return BadRequest(result.Errors);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1)
       => Ok(await  _postService.GetAllAsync(new PaginationParams(page, maxPageSize)));

    [HttpGet("{postId}")]

    public async Task<IActionResult> GetByIdAsync(long postId) 
        => Ok(await _postService.GetByIdAsync(postId));

    [HttpDelete("{postId}")]

    public async Task<IActionResult>DeleteAsync( long postId)
        => Ok( await _postService.DeleteAsync(postId));

    [HttpGet("search")]

    public async Task<IActionResult> SearchAsync([FromQuery] string search) 
        => Ok(await _postService.SearchAsync(search));

    [HttpPut]

    public async Task<IActionResult> UpdateAsync([FromForm] long id , PostUpdateDto postUpdateDto)
        => Ok(await _postService.UpdateAsync(id,postUpdateDto));
}
