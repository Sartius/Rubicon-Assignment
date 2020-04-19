using BlogModelsDTO;

namespace BlogBLL
{
    public interface IBlogLogic 
    {
        public BlogPost PostBlog(BlogPost blogToPost);
        public BlogPost PutBlog(BlogPost blogPostChanges);
        public BlogPost GetBlogBySlug(string slug);
        public TagList GetTagList();
        public BlogPosts GetBlogsByTags(string tag);
        public void DeleteBlog(string slug);
    }
}
