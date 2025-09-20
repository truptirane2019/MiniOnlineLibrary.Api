using Microsoft.EntityFrameworkCore;
using MiniOnlineLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MiniOnlineLibrar.Infrastructure
{
    public class MiniLibraryDbContext : DbContext
    {
        public MiniLibraryDbContext(DbContextOptions<MiniLibraryDbContext> options) : base(options) { }


        public DbSet<User> Users => Set<User>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<BorrowTransaction> BorrowTransactions => Set<BorrowTransaction>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(x => x.UserId);
                b.HasIndex(x => x.Email).IsUnique();
                b.Property(x => x.CreatedAt);//.HasDefaultValueSql("GETUTCDATE()");
            });


            modelBuilder.Entity<Book>(b =>
            {
                b.HasKey(x => x.BookId);
                b.Property(x => x.CreatedAt);//.HasDefaultValueSql("GETUTCDATE()");
            });


            modelBuilder.Entity<BorrowTransaction>(b =>
            {
                b.HasKey(x => x.TransactionId);
                b.Property(x => x.Status).HasConversion<string>();
                b.HasOne(x => x.User).WithMany(u => u.BorrowTransactions).HasForeignKey(x => x.UserId);
                b.HasOne(x => x.Book).WithMany(bk => bk.BorrowTransactions).HasForeignKey(x => x.BookId);
            });
        }
    }
}
