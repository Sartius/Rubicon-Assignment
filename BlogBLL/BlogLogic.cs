using System;
using System.Collections.Generic;
using System.Linq;
using BlogModelsDTO;
using BlogDAL;

namespace BlogBLL
{
    public class BlogLogic : IBlogLogic
    {
        private readonly ISlugfyHelper _slugfyHelper;
        private readonly ICurrentTime _currentTime;
        private readonly IBlogManager _blogManager;

        public BlogLogic(ISlugfyHelper slugfyHelper, ICurrentTime currentTime, IBlogManager blogManager)
        {
            _slugfyHelper = slugfyHelper;
            _currentTime = currentTime;
            _blogManager = blogManager;
        }
        //Global exep?

        /// <summary>
        /// Adds a new post to the database and returns the added post
        /// </summary>
        /// <param name="blogPost"></param>
        /// <returns></returns>
        public BlogPost PostBlog(BlogPost blogPost)
        {
                BlogPost blogToPost = new BlogPost();
                blogToPost.title = blogPost.title;
                blogToPost.description = blogPost.description;
                blogToPost.body = blogPost.body;

                TagList tagList = new TagList();
                tagList.tagList = blogPost.taglist;
                if (!_blogManager.ValidateTags(tagList))
                {
                    throw new ArgumentOutOfRangeException("One or more of the tags listed do not appear in the database.");
                }
                blogToPost.taglist = blogPost.taglist;
                //CHECK TAG LIST
                blogToPost.slug = _slugfyHelper.SlugifyTheTitle(blogToPost.title);
                blogToPost.createdat = _currentTime.CurrentUTCTime();
                blogToPost.updatedat = _currentTime.CurrentUTCTime();
                if(!_blogManager.CheckSlug(blogToPost.slug))
                {
                    blogToPost.slug = _slugfyHelper.GenerateSlugAddon(blogToPost.slug);
                }

                BlogPost blogPosted = _blogManager.PostBlogPost(blogToPost);
                return blogPosted;
        }

        /// <summary>
        /// Uses the url slug to get the post from the database
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public BlogPost GetBlogBySlug(string slug)
        {
            if (_blogManager.CheckSlug(slug))
            {
                throw new KeyNotFoundException("The blog post specified by the url does not exisit");
            }
            BlogPost blogPost = _blogManager.GetBlogPost(slug);
                return blogPost;
        }



        #region TagLogic
        /// <summary>
        /// Returns all the tags found in the database
        /// </summary>
        /// <returns></returns>
        public TagList GetTagList()
        {
                return _blogManager.GetTagList();
        }

        #endregion
        /// <summary>
        /// Updates the specified blog post
        /// </summary>
        /// <param name="blogPostChanges"></param>
        /// <returns></returns>
        public BlogPost PutBlog(BlogPost blogPostChanges)
        {
            if (_blogManager.CheckSlug(blogPostChanges.slug))
            {
                throw new KeyNotFoundException("The blog post specified by the url does not exist");
            }
            //Check if any change was made
            if (blogPostChanges.title == null && blogPostChanges.taglist == null && blogPostChanges.description == null && blogPostChanges.body == null)
                {
                    throw new InvalidOperationException("No changes to the Blog Post were made.");
                }
            //GetOldBlogPost
            BlogPost oldBlogPost = _blogManager.GetBlogPost(blogPostChanges.slug);
            //If title changed
            if (blogPostChanges.title != null)
            {
                _blogManager.DeletePost(oldBlogPost.slug);
                //create new slug
                string newSlug = _slugfyHelper.SlugifyTheTitle(blogPostChanges.title);
                if (!_blogManager.CheckSlug(newSlug))
                {
                    newSlug = _slugfyHelper.GenerateSlugAddon(newSlug);
                }
                oldBlogPost.slug = newSlug;
            }
            //If taglist is changed
            if(blogPostChanges.taglist != null)
            {
                TagList tagList = new TagList();
                tagList.tagList = blogPostChanges.taglist;
                if (!_blogManager.ValidateTags(tagList))
                {
                    throw new ArgumentOutOfRangeException("One or more of the tags listed do not appear in the database.");
                }
                oldBlogPost.taglist = tagList.tagList;
            }
            //If description changes
            if(blogPostChanges.description != null)
            {
                oldBlogPost.description = blogPostChanges.description;
            }
            //If body changes
            if(blogPostChanges.body != null)
            {
                oldBlogPost.body = blogPostChanges.body;
            }

            oldBlogPost.updatedat = _currentTime.CurrentUTCTime();

            return _blogManager.UpdateBlogPost(oldBlogPost);
        }

        /// <summary>
        /// Returns a list of blog posts that contain the specified tags
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public BlogPosts GetBlogsByTags(string tags)
        {
            TagList tagList = new TagList();
            if (tags == null)
            {
                return _blogManager.GetPostsByTag(tagList);
            }
            tagList.tagList = tags.Split(",").ToList();
            //Validate TagList
            if (_blogManager.ValidateTags(tagList))
            {
                return _blogManager.GetPostsByTag(tagList);
            }
            throw new ArgumentOutOfRangeException("One or more of the tags listed do not appear in the database.");
        }

        /// <summary>
        /// Deletes the blog post specified by the slug
        /// </summary>
        /// <param name="slug"></param>
        public void DeleteBlog(string slug)
        {
            if (_blogManager.CheckSlug(slug))
            {
                throw new KeyNotFoundException("The blog post specified by the url does not exist");
            }
            _blogManager.DeletePost(slug);
        }
    }
}
