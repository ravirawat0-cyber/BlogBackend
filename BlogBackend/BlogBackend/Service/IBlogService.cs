using BlogBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogBackend.Service;

public interface IBlogService
{
    Task<ApiResponse<IEnumerable<Blog>>> GetAllBlogs();
    Task<ApiResponse<Blog>> GetBlogById(string id);
    Task<ApiResponse<IEnumerable<string>>> GetAllTags();
    Task<ApiResponse<IEnumerable<Blog>>> GetBlogsByTag(string tag);
    Task<ApiResponse<IEnumerable<Blog>>> GetFavouriteBlogs();
    Task AddBlog(BlogRequest blog, string[] secreteKey);
    Task UpdateBlog(string id, BlogRequest blog, string[] secreteKey);
    Task DeleteBlog(string id, string[] secreteKey);
}