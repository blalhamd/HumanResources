

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Service.Entities.entities;

namespace Service.DataAccess.ConfigurationEntities
{
    public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.ToTable("Prescriptions");

            builder.Property(x => x.dosage).HasColumnType("varchar").HasMaxLength(256).IsRequired(false);
            builder.Property(x => x.duration).HasColumnType("varchar").HasMaxLength(256).IsRequired();
            builder.Property(x => x.medication).HasColumnType("varchar").HasMaxLength(256).IsRequired();
        }
    }

}
