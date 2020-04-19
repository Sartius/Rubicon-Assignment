using AutoMapper;
using BlogModelsDTO;
using System;
using BlogEFModels;
using BlogUOW;
using System.Linq;
using System.Collections.Generic;

namespace BlogDAL
{
    public class BlogManager : IBlogManager
    {
        private IMapper _mapper;

        public BlogManager(IMapper mapper)
        {
            _mapper = mapper;
        }
        public BlogPost PostBlogPost(BlogPost blogToPost)
        {
            using (BlogDatabaseContext context = new BlogDatabaseContext())
            {
                UnitOfWork uow = new UnitOfWork(context);

                Post blogef = _mapper.Map<Post>(blogToPost);

                blogef.Taglist = string.Join(",",blogToPost.taglist);

                if (blogef is null)
                {
                    throw new Exception("Missing Blog Info");
                }

                uow.Blogs.AddNewBlog(blogef);

                uow.Commit();
                return blogToPost;
            }
        }

        public BlogPost GetBlogPost(string slug)
        {
            using (BlogDatabaseContext context = new BlogDatabaseContext())
            {
                UnitOfWork uow = new UnitOfWork(context);

                Post foundBlogPost = uow.Blogs.GetBySlug(slug);

                BlogPost blogPost = _mapper.Map<BlogPost>(foundBlogPost);
                blogPost.taglist = foundBlogPost.Taglist.Split(",").ToList();

                return blogPost;
            }
        }

        public BlogPosts GetPostsByTag(TagList tags)
        {
            using (BlogDatabaseContext context = new BlogDatabaseContext())
            {
                UnitOfWork uow = new UnitOfWork(context);

                var efposts = uow.Blogs.GetBlogsByTag(tags.tagList);

                List<BlogPost> blogPosts = new List<BlogPost>();

                foreach (var efpost in efposts)
                {
                    blogPosts.Add(_mapper.Map<BlogPost>(efpost));
                    blogPosts.Last().taglist = efpost.Taglist.Split(",").ToList();

                }

                blogPosts.OrderByDescending(u => u.updatedat.Date).ThenBy(u => u.updatedat.TimeOfDay);

                BlogPosts blogPostsToReturn = new BlogPosts();

                blogPostsToReturn.blogPosts = blogPosts;

                blogPostsToReturn.postCount = blogPosts.Count();

                return blogPostsToReturn;
            }

        }

        public BlogPost UpdateBlogPost(BlogPost blogPostToUpdate)
        {
            using (BlogDatabaseContext context = new BlogDatabaseContext())
            {
                UnitOfWork uow = new UnitOfWork(context);
                Post blogef = _mapper.Map<Post>(blogPostToUpdate);
                blogef.Taglist = string.Join(",", blogPostToUpdate.taglist);

                if (CheckSlug(blogPostToUpdate.slug))
                {
                    uow.Blogs.AddNewBlog(blogef);
                }
                else 
                uow.Blogs.UpdateBlogPost(blogef);

                uow.Commit();

                return blogPostToUpdate;
            }
        }

        public bool CheckSlug(string slug)
        {
            using (BlogDatabaseContext context = new BlogDatabaseContext())
            {
                UnitOfWork uow = new UnitOfWork(context);

                return uow.Blogs.CheckSlugAvailability(slug);
            }
        }
        public void DeletePost(string slug)
        {
            using (BlogDatabaseContext context = new BlogDatabaseContext())
            {
                UnitOfWork uow = new UnitOfWork(context);
                uow.Blogs.DeleteBlogPost(slug);

                uow.Commit();
            }
        }

        #region TagManager

        public TagList GetTagList()
        {
            using (BlogDatabaseContext context = new BlogDatabaseContext())
            {
                UnitOfWork uow = new UnitOfWork(context);

                TagList tagList = new TagList();

                tagList.tagList = uow.Tags.GetAllTags().Select(u => u.TagName).ToList();

                return tagList;
            }
        }

        public bool ValidateTags(TagList tagList)
        {
            using (BlogDatabaseContext context = new BlogDatabaseContext())
            {
                UnitOfWork uow = new UnitOfWork(context);
                List<Tag> tagsToValidate = new List<Tag>();
                foreach (var tag in tagList.tagList)
                {
                    Tag newTag = new Tag();
                    newTag.TagName = tag;
                     tagsToValidate.Add(newTag);
                }

                bool valid = uow.Tags.ValidateTags(tagsToValidate);
                return valid;
            }
        }
        #endregion
    }
}
