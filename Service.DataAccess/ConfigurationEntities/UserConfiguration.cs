

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Service.Entities.entities;

namespace Service.DataAccess.ConfigurationEntities
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasNoKey();
            builder.ToTable("users");
            builder.Property(x => x.UserName).HasColumnType("varchar").HasMaxLength(128);
            builder.Property(x => x.Password).HasColumnType("varchar").HasMaxLength(128);

        }
    }
   
}
