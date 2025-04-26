using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookReview.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirtsMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BookReview");

            migrationBuilder.CreateTable(
                name: "Books",
                schema: "BookReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: false),
                    YearPublished = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "NULL"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "BookReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "NULL"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                schema: "BookReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "NULL"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "BookReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Photo = table.Column<byte[]>(type: "bytea", nullable: true),
                    Salt = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "NULL"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookCategory",
                schema: "BookReview",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "NULL"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategory", x => new { x.BookId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_BookCategory_Books_BookId",
                        column: x => x.BookId,
                        principalSchema: "BookReview",
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "BookReview",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                schema: "BookReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "NULL"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Books_BookId",
                        column: x => x.BookId,
                        principalSchema: "BookReview",
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "BookReview",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "BookReview",
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "Title", "YearPublished" },
                values: new object[,]
                {
                    { 1, "Gabriel García Márquez", "Una historia inédita y póstuma del Nobel colombiano.", "En agosto nos vemos", 2024 },
                    { 2, "Colleen Hoover", "Una historia de amor intensa y emocional.", "Romper el círculo", 2016 },
                    { 3, "Dot Hutchison", "Thriller psicológico con un escenario oscuro.", "El jardín de las mariposas", 2016 },
                    { 4, "Tracy Wolf", "Novela juvenil sobrenatural.", "Furia", 2020 },
                    { 5, "Mario Mendoza", "Una defensa de la lectura como forma de resistencia.", "Leer es resistir", 2023 },
                    { 6, "Joe Dispenza", "Cambia tu mente para cambiar tu vida.", "Deja de ser tú", 2012 },
                    { 7, "Lydia Cacho", "Investigación sobre redes de explotación infantil.", "Los demonios del edén", 2005 },
                    { 8, "Paul Auster", "Una novela sobre la pérdida y la memoria.", "Baumgartner", 2023 },
                    { 9, "Michael McDowell", "Saga gótica sureña en seis partes.", "Saga Blackwater", 1983 },
                    { 10, "Leila Guerriero", "Retrato periodístico sobre una mujer argentina.", "La llamada, un retrato", 2023 },
                    { 11, "Dan Brown", "Thriller de misterio que combina arte, religión y ciencia.", "El código Da Vinci", 2003 },
                    { 12, "Rebecca Solnit", "Ensayos feministas sobre el mansplaining.", "Los hombres me explican cosas", 2014 },
                    { 13, "J.K. Rowling", "El comienzo de la saga mágica más popular.", "Harry Potter y la piedra filosofal", 1997 },
                    { 14, "Gabriel García Márquez", "Una historia narrada con realismo mágico y fatalismo.", "Crónica de una muerte anunciada", 1981 },
                    { 15, "Carlos Ruiz Zafón", "Una novela gótica sobre libros perdidos y secretos del pasado.", "La sombra del viento", 2001 },
                    { 16, "Julio Cortázar", "Novela experimental e icónica del boom latinoamericano.", "Rayuela", 1963 },
                    { 17, "Antoine de Saint-Exupéry", "Un clásico filosófico sobre la infancia y la sabiduría.", "El principito", 1943 },
                    { 18, "Patrick Rothfuss", "Primer libro de una saga de fantasía sobre Kvothe.", "El nombre del viento", 2007 },
                    { 19, "Stephen King", "Terrorífico relato de infancia, miedo y una entidad maligna.", "It", 1986 },
                    { 20, "Gabriel García Márquez", "La epopeya de la familia Buendía en Macondo.", "Cien años de soledad", 1967 },
                    { 21, "Jane Austen", "Un clásico del romanticismo inglés y la crítica social.", "Orgullo y prejuicio", 1813 },
                    { 22, "Haruki Murakami", "Una historia melancólica de amor, pérdida y juventud.", "Tokio Blues", 1987 },
                    { 23, "Miguel de Cervantes", "La novela más influyente de la literatura española.", "Don Quijote de la Mancha", 1605 },
                    { 24, "Yuval Noah Harari", "Un repaso a la historia de la humanidad.", "Sapiens: De animales a dioses", 2011 },
                    { 25, "John Katzenbach", "Thriller psicológico sobre un juego mortal.", "El psicoanalista", 2002 },
                    { 26, "Paulo Coelho", "Una parábola sobre seguir los sueños personales.", "El alquimista", 1988 },
                    { 27, "Paula Hawkins", "Suspenso psicológico con giros sorprendentes.", "La chica del tren", 2015 },
                    { 28, "Margaret Atwood", "Distopía feminista sobre control y opresión.", "El cuento de la criada", 1985 },
                    { 29, "Aldous Huxley", "Clásico distópico sobre una sociedad controlada por la ciencia.", "Un mundo feliz", 1932 },
                    { 30, "Orson Scott Card", "Novela de ciencia ficción militar y estrategia.", "El juego de Ender", 1985 },
                    { 31, "Suzanne Collins", "Juventud en una distopía brutal.", "Los juegos del hambre", 2008 },
                    { 32, "John Boyne", "Narrativa infantil con trasfondo del Holocausto.", "El niño con el pijama de rayas", 2006 },
                    { 33, "Markus Zusak", "Historia conmovedora durante la Alemania nazi.", "La ladrona de libros", 2005 },
                    { 34, "Khaled Hosseini", "Amistad, culpa y redención en Afganistán.", "Cometas en el cielo", 2003 },
                    { 35, "Carlos Ruiz Zafón", "Un clásico moderno español lleno de misterio.", "La sombra del viento", 2001 },
                    { 36, "Oscar Wilde", "Sobre la belleza, la corrupción y la moral.", "El retrato de Dorian Gray", 1890 },
                    { 37, "Harper Lee", "Drama legal con fuerte crítica al racismo.", "Matar a un ruiseñor", 1960 },
                    { 38, "Fiódor Dostoyevski", "Exploración psicológica del crimen y la redención.", "Crimen y castigo", 1866 },
                    { 39, "Mario Benedetti", "Novela breve sobre el amor y la rutina.", "La tregua", 1960 },
                    { 40, "Juan Rulfo", "Realismo mágico en un pueblo fantasma mexicano.", "Pedro Páramo", 1955 }
                });

            migrationBuilder.InsertData(
                schema: "BookReview",
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Ficción" },
                    { 2, "No Ficción" },
                    { 3, "Novela Romántica" },
                    { 4, "Thriller / Misterio" },
                    { 5, "Fantasía" },
                    { 6, "Clásicos" },
                    { 7, "Juvenil" },
                    { 8, "Ciencia Ficción" },
                    { 9, "Distopía" },
                    { 10, "Realismo Mágico" },
                    { 11, "Ensayo / Filosofía" },
                    { 12, "Histórico" },
                    { 13, "Infantil" },
                    { 14, "Psicología / Autoayuda" }
                });

            migrationBuilder.InsertData(
                schema: "BookReview",
                table: "BookCategory",
                columns: new[] { "BookId", "CategoryId", "Id" },
                values: new object[,]
                {
                    { 1, 10, 0 },
                    { 2, 3, 0 },
                    { 3, 4, 0 },
                    { 4, 7, 0 },
                    { 5, 11, 0 },
                    { 6, 14, 0 },
                    { 7, 11, 0 },
                    { 8, 6, 0 },
                    { 9, 5, 0 },
                    { 10, 12, 0 },
                    { 11, 4, 0 },
                    { 12, 11, 0 },
                    { 13, 5, 0 },
                    { 14, 10, 0 },
                    { 15, 6, 0 },
                    { 16, 6, 0 },
                    { 17, 13, 0 },
                    { 18, 5, 0 },
                    { 19, 4, 0 },
                    { 20, 12, 0 },
                    { 21, 3, 0 },
                    { 21, 6, 0 },
                    { 22, 3, 0 },
                    { 22, 6, 0 },
                    { 23, 6, 0 },
                    { 23, 12, 0 },
                    { 24, 11, 0 },
                    { 25, 4, 0 },
                    { 26, 5, 0 },
                    { 26, 6, 0 },
                    { 27, 4, 0 },
                    { 28, 9, 0 },
                    { 28, 11, 0 },
                    { 29, 9, 0 },
                    { 29, 11, 0 },
                    { 30, 5, 0 },
                    { 30, 8, 0 },
                    { 31, 7, 0 },
                    { 31, 9, 0 },
                    { 32, 4, 0 },
                    { 32, 13, 0 },
                    { 33, 4, 0 },
                    { 34, 12, 0 },
                    { 35, 4, 0 },
                    { 35, 6, 0 },
                    { 36, 6, 0 },
                    { 37, 12, 0 },
                    { 38, 4, 0 },
                    { 39, 3, 0 },
                    { 40, 6, 0 },
                    { 40, 10, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookCategory_CategoryId",
                schema: "BookReview",
                table: "BookCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookId",
                schema: "BookReview",
                table: "Reviews",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                schema: "BookReview",
                table: "Reviews",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookCategory",
                schema: "BookReview");

            migrationBuilder.DropTable(
                name: "Reviews",
                schema: "BookReview");

            migrationBuilder.DropTable(
                name: "Tests",
                schema: "BookReview");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "BookReview");

            migrationBuilder.DropTable(
                name: "Books",
                schema: "BookReview");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "BookReview");
        }
    }
}
