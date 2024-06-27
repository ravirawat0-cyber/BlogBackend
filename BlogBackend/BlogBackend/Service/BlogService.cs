using BlogBackend.Models;
using BlogBackend.Repository;
namespace BlogBackend.Service;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _blogRespository;

    public BlogService(IBlogRepository blogRepository)
    {
        _blogRespository = blogRepository;
    }

    public async Task<ApiResponse<IEnumerable<Blog>>> GetAllBlogs()
    {
        var blogs = await _blogRespository.GetAllBlogs();

        if (blogs == null || blogs.Count() == 0)
            throw new FileNotFoundException("No blogs found in the database.");

        return new ApiResponse<IEnumerable<Blog>> { Message = "Data", Data = blogs };
    }

    public async Task<ApiResponse<Blog>> GetBlogById(string id)
    {
        var blog = await _blogRespository.GetBlogById(id);

        if (blog == null)
            throw new FileNotFoundException($"No blog found with the ID: {id}.");

        return new ApiResponse<Blog> { Message = "Data", Data = blog };
    }

    public async Task<ApiResponse<IEnumerable<string>>> GetAllTags()
    {
        var topics = await _blogRespository.GetAllTags();

        if (topics == null || topics.Count() == 0)
            throw new FileNotFoundException("No topics available at the moment.");

        return new ApiResponse<IEnumerable<string>> { Message = "Data", Data = topics };
    }

    public async Task<ApiResponse<IEnumerable<Blog>>> GetBlogsByTag(string tag)
    {
        var blogs = await _blogRespository.GetBlogsByTag(tag);

        if (blogs == null || blogs.Count() == 0)
            throw new FileNotFoundException($"No blogs found for the topic: {tag}.");

        return new ApiResponse<IEnumerable<Blog>> { Message = "Data", Data = blogs };
    }

    public async Task<ApiResponse<IEnumerable<Blog>>> GetFavouriteBlogs()
    {
        var blogs = await _blogRespository.GetFavouriteBlogs();

        if (blogs == null || blogs.Count() == 0)
            throw new FileNotFoundException("No favorite blogs found.");

        return new ApiResponse<IEnumerable<Blog>> { Message = "Data", Data = blogs };
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
            throw new UnauthorizedAccessException("Invalid secret key provided. Access denied.");
        }
    }

    public async Task UpdateBlog(string id, BlogRequest request, string[] secreteKey)
    {
        if (secreteKey?.Length == 2 && secreteKey[0] == "" && secreteKey[1] == "")
        {
            ValidateRequest(request);
            var blog = new Blog
            {
                Id = id,
                Title = request.Title,
                BlogContent = request.BlogContent,
                Tags = request.Tags
            };
            await _blogRespository.UpdateBlog(id, blog);
        }
        else
        {
            throw new UnauthorizedAccessException("Invalid secret key provided. Access denied.");
        }
    }

    public async Task DeleteBlog(string id, string[] secreteKey)
    {
        if (secreteKey?.Length == 2 && secreteKey[0] == "ravirawat0" && secreteKey[1] == "Alpha@9859")
        {
            await _blogRespository.DeleteBlog(id);
        }
        else
        {
            throw new UnauthorizedAccessException("Invalid secret key provided. Access denied.");
        }
    }

    private void ValidateRequest(BlogRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ArgumentException("Blog title cannot be empty or contain only whitespace.");

        if (string.IsNullOrWhiteSpace(request.BlogContent))
            throw new ArgumentException("Blog content cannot be empty or contain only whitespace.");

        if (request.Tags == null || request.Tags.Count == 0)
            throw new ArgumentException("At least one tag must be provided.");
    }
}
