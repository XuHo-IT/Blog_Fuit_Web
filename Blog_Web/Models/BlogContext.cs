using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Blog_Web.Models;

public partial class BlogContext : DbContext
{
    public BlogContext()
    {
    }

    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<Info> Infos { get; set; }

    public virtual DbSet<News> News { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=Blog;Uid=sa;Password=sa123;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Food>(entity =>
        {
            entity.ToTable("food");

            entity.Property(e => e.PhotoUrl).HasColumnName("photoURl");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<Info>(entity =>
        {
            entity.ToTable("Info");

            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.PhotoUrl).HasColumnName("photoURL");
            entity.Property(e => e.Position).HasColumnName("position");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.ToTable("news");

            entity.Property(e => e.DateUpload).HasColumnType("datetime");
            entity.Property(e => e.PhotoUrl).HasColumnName("photoUrl");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
