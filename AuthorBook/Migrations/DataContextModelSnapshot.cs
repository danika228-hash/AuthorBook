﻿// <auto-generated />
using AuthorBook.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AuthorBook.Migrations;

[DbContext(typeof(AuthorBookDataContext))]
partial class DataContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.6")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("AuthorBook.Models.Author", b =>
            {
                b.Property<int>("AuthorId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer");

                NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AuthorId"));

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("text");

                b.HasKey("AuthorId");

                b.ToTable("Authors");
            });

        modelBuilder.Entity("AuthorBook.Models.Book", b =>
            {
                b.Property<int>("BookId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer");

                NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BookId"));

                b.Property<int>("AuthorId")
                    .HasColumnType("integer");

                b.Property<string>("Title")
                    .IsRequired()
                    .HasColumnType("text");

                b.HasKey("BookId");

                b.HasIndex("AuthorId");

                b.ToTable("Books");
            });

        modelBuilder.Entity("AuthorBook.Models.Book", b =>
            {
                b.HasOne("AuthorBook.Models.Author", "Author")
                    .WithMany("Books")
                    .HasForeignKey("AuthorId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Author");
            });

        modelBuilder.Entity("AuthorBook.Models.Author", b =>
            {
                b.Navigation("Books");
            });
#pragma warning restore 612, 618
    }
}