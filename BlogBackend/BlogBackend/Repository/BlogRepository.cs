using BlogBackend.Models;
using MongoDB.Driver;

namespace BlogBackend.Repository;

public class BlogRepository : IBlogRepository
{
    private readonly DBContext _dbContext;

    public BlogRepository(DBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Blog>> GetAllBlogs()
    {
        return await _dbContext.Blog.Find(_ => true).ToListAsync();
    }

    public async Task<Blog> GetBlogById(string blogId)
    {
        return await _dbContext.Blog.Find(b => b.Id == blogId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<string>> GetAllTags()
    {
        return await _dbContext.Blog.Distinct<string>("tags", Builders<Blog>.Filter.Empty).ToListAsync();
    }

    public async Task<IEnumerable<Blog>> GetBlogsByTag(string tag)
    {
        return await _dbContext.Blog.Find(b => b.Tags.Contains(tag)).ToListAsync();
    }

    public async Task<IEnumerable<Blog>> GetFavouriteBlogs()
    {
        return await _dbContext.Blog.Find(b => b.Favourite == true).ToListAsync();
    }

    public async Task AddBlog(Blog blog)
    {
        await _dbContext.Blog.InsertOneAsync(blog);
    }

    public async Task UpdateBlog(string id, Blog blog)
    {
        await _dbContext.Blog.ReplaceOneAsync(b => b.Id == id, blog);
    }

    public async Task DeleteBlog(string id)
    {
        await _dbContext.Blog.DeleteOneAsync(b => b.Id == id);
    }
}