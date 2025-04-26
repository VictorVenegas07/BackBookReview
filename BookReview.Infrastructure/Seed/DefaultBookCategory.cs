using BookReview.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookReview.Infrastructure.Seed;

public class DefaultBookCategory : IEntityTypeConfiguration<BookCategory>
{
    public void Configure(EntityTypeBuilder<BookCategory> builder)
    {
        var bookCategories = GetBookCategories();
        builder.HasData(bookCategories);
    }

    private List<BookCategory> GetBookCategories()
    {
        return new List<BookCategory>
        {
            new BookCategory { BookId = 1, CategoryId = 10 }, 
            new BookCategory { BookId = 2, CategoryId = 3 },
            new BookCategory { BookId = 3, CategoryId = 4 }, 
            new BookCategory { BookId = 4, CategoryId = 7 },
            new BookCategory { BookId = 5, CategoryId = 11 },
            new BookCategory { BookId = 6, CategoryId = 14 },
            new BookCategory { BookId = 7, CategoryId = 11 },
            new BookCategory { BookId = 8, CategoryId = 6 }, 
            new BookCategory { BookId = 9, CategoryId = 5 }, 
            new BookCategory { BookId = 10, CategoryId = 12 }, 
            new BookCategory { BookId = 11, CategoryId = 4 }, 
            new BookCategory { BookId = 12, CategoryId = 11 }, 
            new BookCategory { BookId = 13, CategoryId = 5 }, 
            new BookCategory { BookId = 14, CategoryId = 10 }, 
            new BookCategory { BookId = 15, CategoryId = 6 }, 
            new BookCategory { BookId = 16, CategoryId = 6 }, 
            new BookCategory { BookId = 17, CategoryId = 13 }, 
            new BookCategory { BookId = 18, CategoryId = 5 }, 
            new BookCategory { BookId = 19, CategoryId = 4 }, 
            new BookCategory { BookId = 20, CategoryId = 12 },
            new BookCategory { BookId = 21, CategoryId = 3 },
            new BookCategory { BookId = 21, CategoryId = 6 },
            new BookCategory { BookId = 22, CategoryId = 3 },
            new BookCategory { BookId = 22, CategoryId = 6 },
            new BookCategory { BookId = 23, CategoryId = 6 },
            new BookCategory { BookId = 23, CategoryId = 12 },
            new BookCategory { BookId = 24, CategoryId = 11 },
            new BookCategory { BookId = 25, CategoryId = 4 }, 
            new BookCategory { BookId = 26, CategoryId = 5 },
            new BookCategory { BookId = 26, CategoryId = 6 },
            new BookCategory { BookId = 27, CategoryId = 4 },  
            new BookCategory { BookId = 28, CategoryId = 9 },
            new BookCategory { BookId = 28, CategoryId = 11 },
            new BookCategory { BookId = 29, CategoryId = 9 },
            new BookCategory { BookId = 29, CategoryId = 11 },
            new BookCategory { BookId = 30, CategoryId = 8 },
            new BookCategory { BookId = 30, CategoryId = 5 },
            new BookCategory { BookId = 31, CategoryId = 9 },
            new BookCategory { BookId = 31, CategoryId = 7 },
            new BookCategory { BookId = 32, CategoryId = 13 },
            new BookCategory { BookId = 32, CategoryId = 4 },
            new BookCategory { BookId = 33, CategoryId = 4 },  
            new BookCategory { BookId = 34, CategoryId = 12 },  
            new BookCategory { BookId = 35, CategoryId = 6 },
            new BookCategory { BookId = 35, CategoryId = 4 },
            new BookCategory { BookId = 36, CategoryId = 6 },  
            new BookCategory { BookId = 37, CategoryId = 12 }, 
            new BookCategory { BookId = 38, CategoryId = 4 }, 
            new BookCategory { BookId = 39, CategoryId = 3 }, 
            new BookCategory { BookId = 40, CategoryId = 10 },
            new BookCategory { BookId = 40, CategoryId = 6 },

        };
    }
}
