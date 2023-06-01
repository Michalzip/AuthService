
using AuthService.Modules.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthService.Shared.ValueObjects.Email;
using AuthService.Shared.ValueObjects.FirstName;
using AuthService.Shared.ValueObjects.LastName;
using AuthService.Shared.ValueObjects.CreatedAt;
using AuthService.Modules.Core.ValueObjects;
using AuthService.Shared.ValueObjects.Password;

namespace AuthService.Modules.infrastructure.EF.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property<Email>("Email")
            .IsRequired()
            .HasConversion(x => x.Value, x => new Email(x));

            builder.Property<Password>("Password")
                .IsRequired()
                .HasConversion(x => x.Value, x => new Password(x));

            builder.Property<FirstName>("FirstName")
            .IsRequired(false)
            .HasConversion(x => x.Value, x => new FirstName(x));

            builder.Property<LastName>("LastName")
                .IsRequired(false)
                .HasConversion(x => x.Value, x => new LastName(x));

            builder.Property<CreatedAt>("CreatedAt")
                .HasConversion(x => x.Value, x => new CreatedAt(x));

            builder.OwnsOne<Provider>("RegistrationProvider", type =>
            {
                type.Property<string>("Name")
                    .IsRequired(false)
                    .HasColumnName("RegistrationProvider");
            });

            builder.ToTable("Users");
        }
    }
}