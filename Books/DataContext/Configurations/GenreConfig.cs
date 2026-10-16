using Books.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.DataContext.Configurations;

public class GenreConfig : BaseEntityConfiguration<Genre>
{
    public override void Configure(EntityTypeBuilder<Genre> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}