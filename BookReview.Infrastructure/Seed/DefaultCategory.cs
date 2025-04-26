using BookReview.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookReview.Infrastructure.Seed;

public class DefaultCategory : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        var categories = GetCategory();
        builder.HasData(categories);
    }

    private List<Category> GetCategory()
    {
        return new List<Category>
        {
            new Category { Id = 1, Name = "Ficción" },
            new Category { Id = 2, Name = "No Ficción" },
            new Category { Id = 3, Name = "Novela Romántica" },
            new Category { Id = 4, Name = "Thriller / Misterio" },
            new Category { Id = 5, Name = "Fantasía" },
            new Category { Id = 6, Name = "Clásicos" },
            new Category { Id = 7, Name = "Juvenil" },
            new Category { Id = 8, Name = "Ciencia Ficción" },
            new Category { Id = 9, Name = "Distopía" },
            new Category { Id = 10, Name = "Realismo Mágico" },
            new Category { Id = 11, Name = "Ensayo / Filosofía" },
            new Category { Id = 12, Name = "Histórico" },
            new Category { Id = 13, Name = "Infantil" },
            new Category { Id = 14, Name = "Psicología / Autoayuda" },
        };
    }
}

