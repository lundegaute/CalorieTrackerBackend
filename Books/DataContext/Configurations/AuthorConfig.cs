using Books.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Books.DataContext.Configurations;

public class AuthorConfiguration: BaseEntityConfiguration<Author>
{
    public override void Configure(EntityTypeBuilder<Author> entity)
    {
        base.Configure(entity);

        entity.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        entity.HasIndex(x => x.LastName);
        entity.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(x => x.Birth)
            .IsRequired();

        entity.Property(x => x.WebUrl);
    }
}
