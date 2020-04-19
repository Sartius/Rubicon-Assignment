using BlogEFModels;
using System.Collections.Generic;

namespace Repositories
{
    public interface IBlogRepository
    {
        public Post GetBySlug(string slug);
        public void AddNewBlog(Post blogpost);
        public bool CheckSlugAvailability(string slug);
        public void DeleteBlogPost(string slug);
        public List<Post> GetBlogsByTag(List<string> searchedTagList);
        public void UpdateBlogPost(Post blogPost);
    }
}
