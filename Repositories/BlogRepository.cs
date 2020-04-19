using BlogEFModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    public class BlogRepository : Repository<Post>, IBlogRepository
    {
        public BlogRepository(BlogDatabaseContext context)
                : base(context)
        {
        }
        public Post GetBySlug(string slug)
        {
            return _dbSet.SingleOrDefault(u => u.Slug == slug);
        }
        public void AddNewBlog(Post blogpost)
        {
            if (blogpost == null)
            {
                throw new ArgumentNullException("Blog data not found");
            }
            _dbSet.Add(blogpost);
        }

        public void UpdateBlogPost(Post blogPost)
        {
            _dbSet.Update(blogPost);
        }

        public void DeleteBlogPost(string slug)
        {
            Post postToDelete = _dbSet.Find(slug);
            _dbSet.Remove(postToDelete);
        }

        public bool CheckSlugAvailability(string slug)
        {
            Post foundPost = _dbSet.Find(slug);
            if (foundPost == null)
                return true;
            return false;
        }

        public List<Post> GetBlogsByTag(List<string> searchedTagList)
        {
            if(searchedTagList == null)
            {
                return _dbSet.ToList();
            }
            List<Post> blogPost_EFs = _dbSet.ToList();
            List<List<string>> tagList = blogPost_EFs.Select(u => u.Taglist.Split(",").ToList()).ToList();

            var tagListbool = _dbSet.ToList().Where(u => u.Taglist.Split(",").ToList().Any(x => searchedTagList.Contains(x))).ToList();

            //blogef.Taglist = string.Join(",", blogPostToUpdate.taglist);


            return tagListbool;
        }

    }
}

