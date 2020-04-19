namespace BlogEFModels
{
    public static class ContextSeeder 
    {

        public static void Seed(BlogDatabaseContext context)
        {

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Tag.Add(new Tag()
            {
                TagName = "2018"
            });

            context.SaveChanges();
        }
    }
}
