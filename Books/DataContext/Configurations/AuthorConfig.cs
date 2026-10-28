using Books.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Books.DataContext.Configurations;

public class AuthorConfiguration: BaseEntityConfiguration<Author>
{
    public override void Configure(EntityTypeBuilder<Author> entity)
    {
        base.Configure(entity);

        entity.Property(x => x.FirstName)
            .HasMaxLength(100);

        entity.HasIndex(x => x.LastName);
        entity.Property(x => x.LastName)
            .HasMaxLength(100);

        entity.Property(x => x.Birth);

        entity.Property(x => x.WebUrl);
    }
}
