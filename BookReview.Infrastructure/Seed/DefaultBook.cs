using BookReview.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookReview.Infrastructure.Seed;

public class DefaultBook : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        var books = GetRealBooks();
        builder.HasData(books);
    }

    private List<Book> GetRealBooks()
    {
        return new List<Book>
        {
            new Book { Id = 1, Title = "En agosto nos vemos", Author = "Gabriel García Márquez", Description = "Una historia inédita y póstuma del Nobel colombiano.", YearPublished = 2024 },
            new Book { Id = 2, Title = "Romper el círculo", Author = "Colleen Hoover", Description = "Una historia de amor intensa y emocional.", YearPublished = 2016 },
            new Book { Id = 3, Title = "El jardín de las mariposas", Author = "Dot Hutchison", Description = "Thriller psicológico con un escenario oscuro.", YearPublished = 2016 },
            new Book { Id = 4, Title = "Furia", Author = "Tracy Wolf", Description = "Novela juvenil sobrenatural.", YearPublished = 2020 },
            new Book { Id = 5, Title = "Leer es resistir", Author = "Mario Mendoza", Description = "Una defensa de la lectura como forma de resistencia.", YearPublished = 2023 },
            new Book { Id = 6, Title = "Deja de ser tú", Author = "Joe Dispenza", Description = "Cambia tu mente para cambiar tu vida.", YearPublished = 2012 },
            new Book { Id = 7, Title = "Los demonios del edén", Author = "Lydia Cacho", Description = "Investigación sobre redes de explotación infantil.", YearPublished = 2005 },
            new Book { Id = 8, Title = "Baumgartner", Author = "Paul Auster", Description = "Una novela sobre la pérdida y la memoria.", YearPublished = 2023 },
            new Book { Id = 9, Title = "Saga Blackwater", Author = "Michael McDowell", Description = "Saga gótica sureña en seis partes.", YearPublished = 1983 },
            new Book { Id = 10, Title = "La llamada, un retrato", Author = "Leila Guerriero", Description = "Retrato periodístico sobre una mujer argentina.", YearPublished = 2023 },
            new Book { Id = 11, Title = "El código Da Vinci", Author = "Dan Brown", Description = "Thriller de misterio que combina arte, religión y ciencia.", YearPublished = 2003 },
            new Book { Id = 12, Title = "Los hombres me explican cosas", Author = "Rebecca Solnit", Description = "Ensayos feministas sobre el mansplaining.", YearPublished = 2014 },
            new Book { Id = 13, Title = "Harry Potter y la piedra filosofal", Author = "J.K. Rowling", Description = "El comienzo de la saga mágica más popular.", YearPublished = 1997 },
            new Book { Id = 14, Title = "Crónica de una muerte anunciada", Author = "Gabriel García Márquez", Description = "Una historia narrada con realismo mágico y fatalismo.", YearPublished = 1981 },
            new Book { Id = 15, Title = "La sombra del viento", Author = "Carlos Ruiz Zafón", Description = "Una novela gótica sobre libros perdidos y secretos del pasado.", YearPublished = 2001 },
            new Book { Id = 16, Title = "Rayuela", Author = "Julio Cortázar", Description = "Novela experimental e icónica del boom latinoamericano.", YearPublished = 1963 },
            new Book { Id = 17, Title = "El principito", Author = "Antoine de Saint-Exupéry", Description = "Un clásico filosófico sobre la infancia y la sabiduría.", YearPublished = 1943 },
            new Book { Id = 18, Title = "El nombre del viento", Author = "Patrick Rothfuss", Description = "Primer libro de una saga de fantasía sobre Kvothe.", YearPublished = 2007 },
            new Book { Id = 19, Title = "It", Author = "Stephen King", Description = "Terrorífico relato de infancia, miedo y una entidad maligna.", YearPublished = 1986 },
            new Book { Id = 20, Title = "Cien años de soledad", Author = "Gabriel García Márquez", Description = "La epopeya de la familia Buendía en Macondo.", YearPublished = 1967 },
            new Book { Id = 21, Title = "Orgullo y prejuicio", Author = "Jane Austen", Description = "Un clásico del romanticismo inglés y la crítica social.", YearPublished = 1813 },
            new Book { Id = 22, Title = "Tokio Blues", Author = "Haruki Murakami", Description = "Una historia melancólica de amor, pérdida y juventud.", YearPublished = 1987 },
            new Book { Id = 23, Title = "Don Quijote de la Mancha", Author = "Miguel de Cervantes", Description = "La novela más influyente de la literatura española.", YearPublished = 1605 },
            new Book { Id = 24, Title = "Sapiens: De animales a dioses", Author = "Yuval Noah Harari", Description = "Un repaso a la historia de la humanidad.", YearPublished = 2011 },
            new Book { Id = 25, Title = "El psicoanalista", Author = "John Katzenbach", Description = "Thriller psicológico sobre un juego mortal.", YearPublished = 2002 },
            new Book { Id = 26, Title = "El alquimista", Author = "Paulo Coelho", Description = "Una parábola sobre seguir los sueños personales.", YearPublished = 1988 },
            new Book { Id = 27, Title = "La chica del tren", Author = "Paula Hawkins", Description = "Suspenso psicológico con giros sorprendentes.", YearPublished = 2015 },
            new Book { Id = 28, Title = "El cuento de la criada", Author = "Margaret Atwood", Description = "Distopía feminista sobre control y opresión.", YearPublished = 1985 },
            new Book { Id = 29, Title = "Un mundo feliz", Author = "Aldous Huxley", Description = "Clásico distópico sobre una sociedad controlada por la ciencia.", YearPublished = 1932 },
            new Book { Id = 30, Title = "El juego de Ender", Author = "Orson Scott Card", Description = "Novela de ciencia ficción militar y estrategia.", YearPublished = 1985 },
            new Book { Id = 31, Title = "Los juegos del hambre", Author = "Suzanne Collins", Description = "Juventud en una distopía brutal.", YearPublished = 2008 },
            new Book { Id = 32, Title = "El niño con el pijama de rayas", Author = "John Boyne", Description = "Narrativa infantil con trasfondo del Holocausto.", YearPublished = 2006 },
            new Book { Id = 33, Title = "La ladrona de libros", Author = "Markus Zusak", Description = "Historia conmovedora durante la Alemania nazi.", YearPublished = 2005 },
            new Book { Id = 34, Title = "Cometas en el cielo", Author = "Khaled Hosseini", Description = "Amistad, culpa y redención en Afganistán.", YearPublished = 2003 },
            new Book { Id = 35, Title = "La sombra del viento", Author = "Carlos Ruiz Zafón", Description = "Un clásico moderno español lleno de misterio.", YearPublished = 2001 },
            new Book { Id = 36, Title = "El retrato de Dorian Gray", Author = "Oscar Wilde", Description = "Sobre la belleza, la corrupción y la moral.", YearPublished = 1890 },
            new Book { Id = 37, Title = "Matar a un ruiseñor", Author = "Harper Lee", Description = "Drama legal con fuerte crítica al racismo.", YearPublished = 1960 },
            new Book { Id = 38, Title = "Crimen y castigo", Author = "Fiódor Dostoyevski", Description = "Exploración psicológica del crimen y la redención.", YearPublished = 1866 },
            new Book { Id = 39, Title = "La tregua", Author = "Mario Benedetti", Description = "Novela breve sobre el amor y la rutina.", YearPublished = 1960 },
            new Book { Id = 40, Title = "Pedro Páramo", Author = "Juan Rulfo", Description = "Realismo mágico en un pueblo fantasma mexicano.", YearPublished = 1955 },


        };
     }
}
