﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
namespace Lab3BookAPI.Model
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options) { }
        public DbSet<Book>? Books { get; set; }
        public DbSet<Genre>? Genres { get; set; }
        public DbSet<Model.Author> Authors { get; set; } = default!;

        public DbSet<BookAuthor> BookAuthors { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(g => g.Genre)
                .WithMany(b => b.BookList)
                .HasForeignKey(g => g.GenreId);

            modelBuilder.Entity<BookAuthor>()
                .HasKey(t => new {t.AuthorId, t.BookId});

            modelBuilder.Entity<BookAuthor>()
             .HasOne(ba => ba.Book)
             .WithMany(b => b.BookAuthors)
             .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);
        }
    }
}
