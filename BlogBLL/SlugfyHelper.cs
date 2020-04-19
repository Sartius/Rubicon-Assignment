using System;
using System.Text;
using System.Text.RegularExpressions;
using BlogDAL;
using Slugify;

namespace BlogBLL
{
    public class SlugfyHelper : ISlugfyHelper
    {
        private readonly IBlogManager _blogManager;

        public SlugfyHelper(IBlogManager blogManager)
        {
            _blogManager = blogManager;
        }
        private string ToSeoFriendly(string title, int maxLength)
        {
            var match = Regex.Match(title.ToLower(), "[\\w]+");
            StringBuilder result = new StringBuilder("");
            bool maxLengthHit = false;
            while (match.Success && !maxLengthHit)
            {
                if (result.Length + match.Value.Length <= maxLength)
                {
                    result.Append(match.Value + "-");
                }
                else
                {
                    maxLengthHit = true;
                    // Handle a situation where there is only one word and it is greater than the max length.  
                    if (result.Length == 0) result.Append(match.Value.Substring(0, maxLength));
                }
                match = match.NextMatch();
            }
            // Remove trailing '-'  
            if (result[result.Length - 1] == '-') result.Remove(result.Length - 1, 1);
            return result.ToString();
        }

        public string SlugifyTheTitle(string title)
        {
            SlugHelper slugHelper = new SlugHelper();
            string slugfyed = slugHelper.GenerateSlug(title);
            //check unique return reccomend
            return slugfyed;
        }

        public string GenerateSlugAddon(string slugToMakeUnique)
        {
            string newSlug;
            do
            {
                string substring = Guid.NewGuid().ToString("n").Substring(0, 8);
                newSlug = slugToMakeUnique + substring;
            } while (_blogManager.CheckSlug(newSlug) == false);
            return newSlug;
        }
    }
}
