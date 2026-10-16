using Books.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.DataContext.Configurations;

public class BookConfig : BaseEntityConfiguration<Book>
{
    public override void Configure(EntityTypeBuilder<Book> builder)
    {
        base.Configure(builder);

        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Series)
            .HasMaxLength(100);

        builder.Property(b => b.Pages)
            .IsRequired();

        builder.Property(b => b.StartedReading)
            .IsRequired();
        builder.Property(b => b.EndedReading)
            .IsRequired();

        builder.Property(b => b.ReleaseYear);

        // Relations
        builder.HasOne(b => b.Author)
            .WithMany()
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Genre)
            .WithMany()
            .HasForeignKey(b => b.GenreId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}