using System.Collections.Generic;
using BlogEFModels;

namespace Repositories
{
    public interface ITagRepository
    {
        public List<Tag> GetAllTags();
        public bool ValidateTags(List<Tag> tags);
    }
}
