using Library.CodeFirstDatabase.Entities;
using Library.CodeFirstDatabase.Enum;
using Library.CodeFirstDatabase.Views;
using Microsoft.EntityFrameworkCore;


namespace Library.Entities
{
    public class LibraryDbContext : DbContext
    {
       private string _connectionString = "Server = localhost; Database=Library;Port=5432;User Id=postgres;Password=1234";

       public DbSet<Book> Books { get; set; }
       public DbSet<Author> Authors { get; set; }
       public DbSet<Category> Categories { get; set; }
       public DbSet<BookInstance> Book_Instances { get; set; }
       public DbSet<Spot> Spots { get; set; }
       public DbSet<Borrow> Borrows { get; set; }
       public DbSet<User> Users { get; set; }
       public DbSet<Password> Passwords { get; set; }
       public DbSet<ProposedBook> Proposed_Books { get; set; }
       public DbSet<LogEntry> Log_Entries { get; set; }
       public DbSet<BookView> BookView { get; set; }
        public DbSet<AuthorView> AuthorView { get; set; }
        public DbSet<CategoryView> CategoryView { get; set; }
        public DbSet<BorrowView> BorrowView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(b =>
            {
                b.Property(b => b.Title)
                    .IsRequired();
                b.Property(b => b.ISBN)
                    .HasMaxLength(13);
                b.Property(b => b.Description)
                    .HasMaxLength(400);
               

                b.HasKey(b => b.Id);

                b.HasMany(b => b.Authors)
                    .WithMany(a => a.Books);
                b.HasMany(b => b.Categories)
                    .WithMany(c => c.Books);
            });

            modelBuilder.Entity<Author>(a =>
            {
                a.Property(b => b.FirstName)
                    .IsRequired()
                    .HasMaxLength(25);
                a.Property(b => b.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });


            modelBuilder.Entity<BookInstance>(bi =>
            {
                bi.Property(bi => bi.Status)
                    .HasMaxLength(11);
                bi.Property(bi => bi.Status)
                    .HasConversion(c => c.ToString(),
                        c => Enum.Parse<Status>(c));

                bi.HasOne(bi => bi.Book)
                    .WithMany(b => b.BookInstances)
                    .HasForeignKey(bi => bi.BookId);
                bi.HasOne(bi => bi.Spot)
                    .WithMany(s => s.BookInstances)
                    .HasForeignKey(bi => bi.SpotId);
            });

            modelBuilder.Entity<Borrow>(br =>
            {
                br.HasOne(br => br.BookInstance)
                    .WithMany(bi => bi.Borrows)
                    .HasForeignKey(br => br.BookInstanceId);
                br.HasOne(br => br.User)
                    .WithMany(u => u.Borrows)
                    .HasForeignKey(br => br.UserId);

            });


            modelBuilder.Entity<Spot>(sp =>
            {
                sp.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(25);
                sp.Property(s => s.Building)
                    .IsRequired()
                    .HasMaxLength(50);
               
            });
            
            modelBuilder.Entity<User>(u =>
            {
                u.Property(u => u.FirstName)
                    .IsRequired()
                    .HasMaxLength(25);
                u.Property(u => u.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
                u.HasOne(u => u.Password)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(u => u.PasswordId);
                u.HasMany(u => u.ProposedBooks)
                    .WithMany(p => p.Users);
            });

            modelBuilder.Entity<LogEntry>(lg =>
            {
                lg.HasOne(lg => lg.User)
                    .WithMany(u => u.LogEntries)
                    .HasForeignKey(lg => lg.UserId);
                lg.Property(l=>l.Operation)
                        .HasConversion(c => c.ToString(),
                    c => Enum.Parse<OperationType>(c));
            });
            
            modelBuilder.Entity<BookView>()
                .ToView(nameof(BookView))
                .HasKey(b => b.Id);
            modelBuilder.Entity<AuthorView>()
                .ToView(nameof(AuthorView))
                .HasKey(a => a.Id);
            modelBuilder.Entity<CategoryView>()
                .ToView(nameof(CategoryView))
                .HasKey(c => c.Id);
            modelBuilder.Entity<BorrowView>()
                .ToView(nameof(BorrowView))
                .HasKey(b => b.Id);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }
}
