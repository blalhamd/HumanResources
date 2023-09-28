

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Service.Entities.entities;
using Service.Comman.Enums;

namespace Service.DataAccess.ConfigurationEntities
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.Property(x => x.address).HasColumnType("varchar").HasMaxLength(256).IsRequired(false);
            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(256).IsRequired();
            builder.Property(x => x.phone).HasColumnType("varchar").HasMaxLength(256).IsRequired();
            builder.Property(x => x.gender).HasColumnType("Gender").HasConversion(
                                               v => v.ToString(),
                                               v => (Gender)Enum.Parse(typeof(Gender), v)
                                           ).
                                           IsRequired();

        }
    }
    
}
