using BlogBackend.Models;
using System.Threading.Tasks;

namespace BlogBackend.Service;

public interface IBlogService
{
    Task<IEnumerable<Blog>> GetAllBlogs();
    Task<Blog> GetBlogById(string id);
    Task<IEnumerable<string>> GetAllTopics();
    Task<IEnumerable<Blog>> GetBlogsByTopic(string topic);
    Task<IEnumerable<Blog>> GetFavouriteBlogs();
    Task AddBlog(BlogRequest blog, string[] secreteKey);
    Task UpdateBlog(string id, BlogRequest blog, string[] secreteKey);
    Task DeleteBlog(string id);
}