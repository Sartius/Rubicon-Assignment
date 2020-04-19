using BlogEFModels;
using Repositories;
using System;

namespace BlogUOW
{
    public class UnitOfWork
    {
        private readonly BlogDatabaseContext _context;
        private BlogRepository _blogs;
        private TagRepository _tags;
        public UnitOfWork(BlogDatabaseContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("Context was not supplied");
            }
            _context = context;
        }
        #region IUnitOfWork Members


        public IBlogRepository Blogs
        {
            get
            {
                if (_blogs == null)
                {
                    _blogs = new BlogRepository(_context);
                }
                return _blogs;
            }
        }

        public ITagRepository Tags
        {
            get
            {
                if (_tags == null)
                {
                    _tags = new TagRepository(_context);
                }
                return _tags;
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
        #endregion
    }
}
