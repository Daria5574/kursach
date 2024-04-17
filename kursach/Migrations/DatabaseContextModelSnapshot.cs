﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using kursach.Model;

#nullable disable

namespace kursach.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("kursach.Model.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ID_User")
                        .HasColumnType("int");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ID_User");

                    b.ToTable("author");
                });

            modelBuilder.Entity("kursach.Model.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("About_The_Book")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Age_Rating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cover")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Date_Of_Writing")
                        .HasColumnType("int");

                    b.Property<int?>("ID_Author")
                        .HasColumnType("int");

                    b.Property<int?>("ID_User")
                        .HasColumnType("int");

                    b.Property<string>("ISBN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Is_Favorite")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Number_Of_Printed_Pages")
                        .HasColumnType("int");

                    b.Property<string>("The_Path_To_The_File")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("The_Year_Of_Publishing")
                        .HasColumnType("int");

                    b.Property<string>("Time_To_Read")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ID_Author");

                    b.HasIndex("ID_User");

                    b.ToTable("book");
                });

            modelBuilder.Entity("kursach.Model.Book_Bookshelf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ID_Book")
                        .HasColumnType("int");

                    b.Property<int?>("ID_Bookshelf")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ID_Book");

                    b.HasIndex("ID_Bookshelf");

                    b.ToTable("book_bookshelf");
                });

            modelBuilder.Entity("kursach.Model.Book_Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ID_Book")
                        .HasColumnType("int");

                    b.Property<int?>("ID_Category")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ID_Book");

                    b.HasIndex("ID_Category");

                    b.ToTable("book_category");
                });

            modelBuilder.Entity("kursach.Model.Book_Theme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ID_Book")
                        .HasColumnType("int");

                    b.Property<int?>("ID_Theme")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ID_Book");

                    b.HasIndex("ID_Theme");

                    b.ToTable("book_theme");
                });

            modelBuilder.Entity("kursach.Model.Bookshelf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Color_Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_User")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ID_User");

                    b.ToTable("bookshelf");
                });

            modelBuilder.Entity("kursach.Model.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("category");
                });

            modelBuilder.Entity("kursach.Model.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ID_Category")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ID_Category");

                    b.ToTable("genre");
                });

            modelBuilder.Entity("kursach.Model.Theme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("theme");
                });

            modelBuilder.Entity("kursach.Model.User", b =>
                {
                    b.Property<int>("ID_User")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_User"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_User");

                    b.ToTable("user");
                });

            modelBuilder.Entity("kursach.Model.Author", b =>
                {
                    b.HasOne("kursach.Model.User", "User")
                        .WithMany("Author")
                        .HasForeignKey("ID_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("kursach.Model.Book", b =>
                {
                    b.HasOne("kursach.Model.Author", "Author")
                        .WithMany("Book")
                        .HasForeignKey("ID_Author");

                    b.HasOne("kursach.Model.User", "User")
                        .WithMany("Book")
                        .HasForeignKey("ID_User");

                    b.Navigation("Author");

                    b.Navigation("User");
                });

            modelBuilder.Entity("kursach.Model.Book_Bookshelf", b =>
                {
                    b.HasOne("kursach.Model.Book", "Book")
                        .WithMany("Book_Bookshelf")
                        .HasForeignKey("ID_Book");

                    b.HasOne("kursach.Model.Bookshelf", "Bookshelf")
                        .WithMany("Book_Bookshelf")
                        .HasForeignKey("ID_Bookshelf");

                    b.Navigation("Book");

                    b.Navigation("Bookshelf");
                });

            modelBuilder.Entity("kursach.Model.Book_Category", b =>
                {
                    b.HasOne("kursach.Model.Book", "Book")
                        .WithMany("Book_Category")
                        .HasForeignKey("ID_Book");

                    b.HasOne("kursach.Model.Category", "Category")
                        .WithMany("Book_Category")
                        .HasForeignKey("ID_Category");

                    b.Navigation("Book");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("kursach.Model.Book_Theme", b =>
                {
                    b.HasOne("kursach.Model.Book", "Book")
                        .WithMany("Book_Theme")
                        .HasForeignKey("ID_Book");

                    b.HasOne("kursach.Model.Theme", "Theme")
                        .WithMany("Book_Theme")
                        .HasForeignKey("ID_Theme");

                    b.Navigation("Book");

                    b.Navigation("Theme");
                });

            modelBuilder.Entity("kursach.Model.Bookshelf", b =>
                {
                    b.HasOne("kursach.Model.User", "User")
                        .WithMany("Bookshelf")
                        .HasForeignKey("ID_User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("kursach.Model.Genre", b =>
                {
                    b.HasOne("kursach.Model.Category", "Category")
                        .WithMany("Genre")
                        .HasForeignKey("ID_Category")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("kursach.Model.Author", b =>
                {
                    b.Navigation("Book");
                });

            modelBuilder.Entity("kursach.Model.Book", b =>
                {
                    b.Navigation("Book_Bookshelf");

                    b.Navigation("Book_Category");

                    b.Navigation("Book_Theme");
                });

            modelBuilder.Entity("kursach.Model.Bookshelf", b =>
                {
                    b.Navigation("Book_Bookshelf");
                });

            modelBuilder.Entity("kursach.Model.Category", b =>
                {
                    b.Navigation("Book_Category");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("kursach.Model.Theme", b =>
                {
                    b.Navigation("Book_Theme");
                });

            modelBuilder.Entity("kursach.Model.User", b =>
                {
                    b.Navigation("Author");

                    b.Navigation("Book");

                    b.Navigation("Bookshelf");
                });
#pragma warning restore 612, 618
        }
    }
}
