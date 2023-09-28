

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Service.Entities.entities;
using Service.Comman.Enums;

namespace Service.DataAccess.ConfigurationEntities
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bills");
            builder.Property(x => x.status).HasConversion(
                                               v => v.ToString(),
                                               v => (StatusBill)Enum.Parse(typeof(StatusBill), v)
                                           ).
                                           IsRequired();

            builder.Property(x => x.amount).HasPrecision(15, 2).IsRequired();
        }
    }
    
}
