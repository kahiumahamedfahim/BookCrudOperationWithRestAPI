using Microsoft.EntityFrameworkCore;
using BookCrudOperationWIthRestAPI.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookCrudOperationWIthRestAPI.DataAcess
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(x => x.Id);
            builder.Property(b=>b.Title)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(b => b.Author)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(b => b.PublishedDate)
                .HasColumnType("date")
                .IsRequired();

        }
    }
}
