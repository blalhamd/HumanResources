

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Service.Entities.entities;
using Service.Comman.Enums;

namespace Service.DataAccess.ConfigurationEntities
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");

            builder.Property(x => x.status).HasConversion(
                                               v => v.ToString(),
                                               v => (StatusAppointment)Enum.Parse(typeof(StatusAppointment), v)
                                           ).
                                           IsRequired();

            // builder.Property(x => x.status).HasColumnType("StatusAppointment"); Note: sql server does't knowe data type StatusAppointment
        }
    }

}
