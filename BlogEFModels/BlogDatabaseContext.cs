using Microsoft.EntityFrameworkCore;

namespace BlogEFModels
{
    public partial class BlogDatabaseContext : DbContext
    {
        public BlogDatabaseContext()
        {
        }

        public BlogDatabaseContext(DbContextOptions<BlogDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlite("Filename=BlogDatabase.db;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.Slug);

                entity.Property(e => e.Slug).HasColumnName("slug");

                entity.Property(e => e.Blogbody)
                    .IsRequired()
                    .HasColumnName("blogbody");

                entity.Property(e => e.Blogdescription).HasColumnName("blogdescription");

                entity.Property(e => e.Blogtitle)
                    .IsRequired()
                    .HasColumnName("blogtitle");

                entity.Property(e => e.Createdat)
                    .IsRequired()
                    .HasColumnName("createdat");

                entity.Property(e => e.Taglist)
                    .IsRequired()
                    .HasColumnName("taglist");

                entity.Property(e => e.Updatedat)
                    .IsRequired()
                    .HasColumnName("updatedat");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.TagName);

                entity.Property(e => e.TagName).HasColumnName("tagName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
