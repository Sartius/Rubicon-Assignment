using System;
using System.Collections.Generic;
using BlogBLL;
using BlogCoreAPI.ViewModels;
using BlogModelsDTO;
using Microsoft.AspNetCore.Mvc;

namespace BlogCoreAPI.Controllers
{
    [Route("api/[controller]")]

    public class BlogController : Controller
    {
        private readonly IBlogLogic _blogLogic;
        public BlogController(IBlogLogic blogLogic)
        {
            _blogLogic = blogLogic;
        }
        [HttpGet("{slug}")]
        public ActionResult GetBlogPost(string slug)
        {
            try
            {
                BlogPost blogPost = _blogLogic.GetBlogBySlug(slug);
                if (blogPost == null)
                {
                    return new BadRequestResult();
                }
                BlogPostVM blogPostToSend = new BlogPostVM();
                blogPostToSend.blogPost = blogPost;
                return Ok(blogPostToSend);
            }
            catch(KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult GetBlogPosts(string tag)
        {
            try
            {
                BlogPosts blogPost = _blogLogic.GetBlogsByTags(tag);
                if (blogPost == null)
                {
                    return new BadRequestResult();
                }
                return Ok(blogPost);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public IActionResult Posts([FromBody] BlogPostVM blogPost)
        {
            try
            {
                BlogPost blogPosted = _blogLogic.PostBlog(blogPost.blogPost);
                BlogPostVM blogPostToSend = new BlogPostVM();
                blogPostToSend.blogPost = blogPosted;
                return Ok(blogPostToSend);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {

                throw;
            }

        }

        // PUT api/values/5
        [HttpPut("{slug}")]
        public IActionResult Put(string slug, [FromBody] BlogPostVM blogPost)
        {
            try
            {
                blogPost.blogPost.slug = slug;
                BlogPost blogPosted = _blogLogic.PutBlog(blogPost.blogPost);
                BlogPostVM blogPostToSend = new BlogPostVM();
                blogPostToSend.blogPost = blogPosted;
                return Ok(blogPostToSend);
            }
            catch (KeyNotFoundException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpDelete("{slug}")]
        public ActionResult DeleteBlogPost(string slug)
        {
            try
            {
                _blogLogic.DeleteBlog(slug);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
