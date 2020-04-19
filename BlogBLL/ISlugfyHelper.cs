namespace BlogBLL
{
    public interface ISlugfyHelper
    {
        public string SlugifyTheTitle(string title);
        public string GenerateSlugAddon(string slugToMakeUnique);
    }
}
