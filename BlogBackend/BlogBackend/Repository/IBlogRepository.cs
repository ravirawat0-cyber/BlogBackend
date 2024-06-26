﻿using BlogBackend.Models;

namespace BlogBackend.Repository;

public interface IBlogRepository
{
    Task<IEnumerable<Blog>> GetAllBlogs();
    Task<Blog> GetBlogById(string blogId);
    Task<IEnumerable<string>> GetAllTopics();
    Task<IEnumerable<Blog>> GetBlogsByTopic(string topic);
    Task<IEnumerable<Blog>> GetFavouriteBlogs();
    Task AddBlog(Blog blog);
    Task UpdateBlog(string id, Blog blog);
    Task DeleteBlog(string id);
}