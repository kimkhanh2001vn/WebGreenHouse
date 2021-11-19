using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Model.EF
{
    public partial class GreenHouseDB : DbContext
    {
        public GreenHouseDB()
            : base("name=GreenHouseDB")
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Footer> Footers { get; set; }
        public virtual DbSet<MenuGruop> MenuGruops { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Messenger> Messengers { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<PostCategory> PostCategorys { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<ProductCategory> ProductCategorys { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Sell> Sells { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<Structure> Structures { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuGruop>()
                .HasMany(e => e.Menus)
                .WithRequired(e => e.MenuGruop)
                .HasForeignKey(e => e.GruopID);

            modelBuilder.Entity<PostCategory>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<PostCategory>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.PostCategory)
                .HasForeignKey(e => e.CategoryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Tags1)
                .WithMany(e => e.Posts)
                .Map(m => m.ToTable("PostTags").MapLeftKey("PostID").MapRightKey("TagID"));

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.ProductCategory)
                .HasForeignKey(e => e.CategoryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.CreatedByID)
                .WillCascadeOnDelete(false);
        }
    }
}
