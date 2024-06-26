using Amazon.Runtime.Internal;
using BlogBackend.Models;
using BlogBackend.Repository;
using MongoDB.Driver.Core.Operations;

namespace BlogBackend.Service;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _blogRespository;

    public BlogService(IBlogRepository blogRepository)
    {
        _blogRespository = blogRepository;
    }

    public async Task<IEnumerable<Blog>> GetAllBlogs()
    {
        var blogs = await _blogRespository.GetAllBlogs();
        if (blogs == null)
            throw new FileNotFoundException("No data present.");
        return blogs;
    }

    public async Task<Blog> GetBlogById(string id)
    {
        var blog = await _blogRespository.GetBlogById(id);
        if (blog == null)
            throw new FileNotFoundException("No data present with given ID.");
        return blog;
    }

    public Task<IEnumerable<string>> GetAllTopics()
    {
        var topics = _blogRespository.GetAllTopics();
        if (topics == null)
            throw new FileNotFoundException("No topics present.");
        return topics;
    }

    public Task<IEnumerable<Blog>> GetBlogsByTopic(string topic)
    {
        var blogs = _blogRespository.GetBlogsByTopic(topic);
        if (blogs == null)
            throw new FileNotFoundException("No blogs present for given topics.");
        return blogs;
    }

    public Task<IEnumerable<Blog>> GetFavouriteBlogs()
    {
        var blogs = _blogRespository.GetFavouriteBlogs();
        if (blogs == null)
            throw new FileNotFoundException("No favourite blog present.");
        return blogs;
    }

    public async Task AddBlog(BlogRequest request, string[] secreteKey)
    {
        if (secreteKey?.Length == 2 && secreteKey[0] == "" && secreteKey[1] == "")
        {
            ValidateRequest(request);
            var blog = new Blog
            {
                Title = request.Title,
                BlogContent = request.BlogContent,
                Tags = request.Tags
            };
            await _blogRespository.AddBlog(blog);
        }
        else
        {
            throw new UnauthorizedAccessException("Invalid secret key");
        }
    }

    public async Task UpdateBlog(string id, BlogRequest request, string[] secreteKey)
    {
        if (secreteKey?.Length == 2 && secreteKey[0] == "ravirawat0" && secreteKey[1] == "Alpha@9859")
        {
            ValidateRequest(request);
            var blog = new Blog
            {
                Title = request.Title,
                BlogContent = request.BlogContent,
                Tags = request.Tags
            };
            await _blogRespository.UpdateBlog(id, blog);
        }
        else
        {
            throw new UnauthorizedAccessException("Invalid secret key");
        }
    }

    public async Task DeleteBlog(string id)
    {
        await _blogRespository.DeleteBlog(id);
    }

    public void ValidateRequest(BlogRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ArgumentException("Title cannot be empty or contain whitespace.");

        if (string.IsNullOrWhiteSpace(request.BlogContent))
            throw new ArgumentException("Content cannot be empty or contain whitespace.");

        if (request.Tags == null || request.Tags.Count == 0)
            throw new ArgumentException("Tags cannot be empty.");
    }
}