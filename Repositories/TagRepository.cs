using BlogEFModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(BlogDatabaseContext context)
                : base(context)
        {
        }
        public List<Tag> GetAllTags()
        {
            return _dbSet.ToList();
        }
        public bool ValidateTags(List<Tag> tags)
        {
            //Test
            //List<Tag> pppupu = _dbSet.ToList();

            bool isValid = tags.All(u => _dbSet.Select(x => x.TagName).Contains(u.TagName));
            return isValid;
        }
    }
}
