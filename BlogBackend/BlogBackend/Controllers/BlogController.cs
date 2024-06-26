using BlogBackend.Models;
using BlogBackend.Service;
using Microsoft.AspNetCore.Mvc;

namespace BlogBackend.Controllers;

[Route("[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly IBlogService _blogService;

    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllBlogs()
    {
        var blogs = await _blogService.GetAllBlogs();
        return Ok(blogs);
    }

    [HttpGet("{blogId}")]
    public async Task<ActionResult> GetBlogById(string blogId)
    {
        var blog = await _blogService.GetBlogById(blogId);
        return Ok(blog);
    }

    [HttpGet("topics")]
    public async Task<ActionResult> GetAllTopics()
    {
        var tags = await _blogService.GetAllTopics();
        return Ok(tags);
    }

    [HttpGet("topic/{topic}")]
    public async Task<ActionResult> GetBlogsByTopic(string topic)
    {
        var blogs = await _blogService.GetBlogsByTopic(topic);
        return Ok(blogs);
    }

    [HttpGet("favourites")]
    public async Task<ActionResult> GetFavouriteBlogs()
    {
        var blogs = await _blogService.GetFavouriteBlogs();
        return Ok(blogs);
    }

    [HttpPost]
    public async Task<ActionResult> CreateBlog([FromBody] BlogRequest request, [FromHeader] string[] secreteKey)
    {
        await _blogService.AddBlog(request, secreteKey);
        return NoContent();
    }

    [HttpPut("{blogId}")]
    public async Task<ActionResult> UpdateBlog(string blogId, [FromBody] BlogRequest request, string[] secreteKey)
    {
        await _blogService.UpdateBlog(blogId, request, secreteKey);
        return NoContent();
    }

    [HttpDelete("{blogId}")]
    public async Task<ActionResult> DeleteBlog(string blogId)
    {
        await _blogService.DeleteBlog(blogId);
        return NoContent();
    }
}