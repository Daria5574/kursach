using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kursach.Migrations
{
    public partial class InitializeCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "theme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_theme", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    ID_User = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.ID_User);
                });

            migrationBuilder.CreateTable(
                name: "genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Category = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_genre_category_ID_Category",
                        column: x => x.ID_Category,
                        principalTable: "category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID_User = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author", x => x.Id);
                    table.ForeignKey(
                        name: "FK_author_user_ID_User",
                        column: x => x.ID_User,
                        principalTable: "user",
                        principalColumn: "ID_User");
                });

            migrationBuilder.CreateTable(
                name: "bookshelf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_User = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color_Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookshelf", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bookshelf_user_ID_User",
                        column: x => x.ID_User,
                        principalTable: "user",
                        principalColumn: "ID_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_Author = table.Column<int>(type: "int", nullable: true),
                    The_Path_To_The_File = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number_Of_Printed_Pages = table.Column<int>(type: "int", nullable: true),
                    Date_Of_Writing = table.Column<int>(type: "int", nullable: true),
                    The_Year_Of_Publishing = table.Column<int>(type: "int", nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time_To_Read = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    About_The_Book = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age_Rating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Favorite = table.Column<int>(type: "int", nullable: false),
                    ID_User = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_book_author_ID_Author",
                        column: x => x.ID_Author,
                        principalTable: "author",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_book_user_ID_User",
                        column: x => x.ID_User,
                        principalTable: "user",
                        principalColumn: "ID_User");
                });

            migrationBuilder.CreateTable(
                name: "book_bookshelf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Book = table.Column<int>(type: "int", nullable: true),
                    ID_Bookshelf = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book_bookshelf", x => x.Id);
                    table.ForeignKey(
                        name: "FK_book_bookshelf_book_ID_Book",
                        column: x => x.ID_Book,
                        principalTable: "book",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_book_bookshelf_bookshelf_ID_Bookshelf",
                        column: x => x.ID_Bookshelf,
                        principalTable: "bookshelf",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "book_category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Book = table.Column<int>(type: "int", nullable: true),
                    ID_Category = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book_category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_book_category_book_ID_Book",
                        column: x => x.ID_Book,
                        principalTable: "book",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_book_category_category_ID_Category",
                        column: x => x.ID_Category,
                        principalTable: "category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "book_theme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Book = table.Column<int>(type: "int", nullable: true),
                    ID_Theme = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book_theme", x => x.Id);
                    table.ForeignKey(
                        name: "FK_book_theme_book_ID_Book",
                        column: x => x.ID_Book,
                        principalTable: "book",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_book_theme_theme_ID_Theme",
                        column: x => x.ID_Theme,
                        principalTable: "theme",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_author_ID_User",
                table: "author",
                column: "ID_User");

            migrationBuilder.CreateIndex(
                name: "IX_book_ID_Author",
                table: "book",
                column: "ID_Author");

            migrationBuilder.CreateIndex(
                name: "IX_book_ID_User",
                table: "book",
                column: "ID_User");

            migrationBuilder.CreateIndex(
                name: "IX_book_bookshelf_ID_Book",
                table: "book_bookshelf",
                column: "ID_Book");

            migrationBuilder.CreateIndex(
                name: "IX_book_bookshelf_ID_Bookshelf",
                table: "book_bookshelf",
                column: "ID_Bookshelf");

            migrationBuilder.CreateIndex(
                name: "IX_book_category_ID_Book",
                table: "book_category",
                column: "ID_Book");

            migrationBuilder.CreateIndex(
                name: "IX_book_category_ID_Category",
                table: "book_category",
                column: "ID_Category");

            migrationBuilder.CreateIndex(
                name: "IX_book_theme_ID_Book",
                table: "book_theme",
                column: "ID_Book");

            migrationBuilder.CreateIndex(
                name: "IX_book_theme_ID_Theme",
                table: "book_theme",
                column: "ID_Theme");

            migrationBuilder.CreateIndex(
                name: "IX_bookshelf_ID_User",
                table: "bookshelf",
                column: "ID_User");

            migrationBuilder.CreateIndex(
                name: "IX_genre_ID_Category",
                table: "genre",
                column: "ID_Category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "book_bookshelf");

            migrationBuilder.DropTable(
                name: "book_category");

            migrationBuilder.DropTable(
                name: "book_theme");

            migrationBuilder.DropTable(
                name: "genre");

            migrationBuilder.DropTable(
                name: "bookshelf");

            migrationBuilder.DropTable(
                name: "book");

            migrationBuilder.DropTable(
                name: "theme");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "author");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
