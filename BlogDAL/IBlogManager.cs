using BlogModelsDTO;

namespace BlogDAL
{
    public interface IBlogManager
    {
        public BlogPost PostBlogPost(BlogPost blogToPost);
        public BlogPost GetBlogPost(string slug);
        public TagList GetTagList();
        public bool CheckSlug(string slug);
        public void DeletePost(string slug);
        public BlogPost UpdateBlogPost(BlogPost blogPostToUpdate);
        public bool ValidateTags(TagList tagList);
        public BlogPosts GetPostsByTag(TagList tags);
    }
}
