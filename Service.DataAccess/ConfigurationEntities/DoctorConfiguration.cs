

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Service.Entities.entities;
using Service.Comman.Enums;

namespace Service.DataAccess.ConfigurationEntities
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");

            builder.Property(x => x.contact).HasColumnType("varchar").HasMaxLength(256).IsRequired();
            builder.Property(x => x.license).HasColumnType("varchar").HasMaxLength(256).IsRequired();
            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(256).IsRequired();
            builder.Property(x => x.specialty).HasConversion(
                                               v => v.ToString(),
                                               v => (Specialty)Enum.Parse(typeof(Specialty), v)
                                           ).
                                           IsRequired(false);
        }
    }

}
