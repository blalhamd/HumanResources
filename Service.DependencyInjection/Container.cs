
using Framework.core.comman;
using Framework.core.IRepositories;
using Framework.core.IRerepositories.Base;
using Framework.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Business.comman;
using Service.Business.Services;
using Service.Business.Services.Comman;
using Service.Core.AutoMapper;
using Service.Core.IRepositories;
using Service.Core.IRepositories.Base;
using Service.Core.IServices;
using Service.DataAccess.APPDBCONTEXT;
using Service.DataAccess.Repositories;
using Service.DataAccess.Repositories.Base;
using Service.Entities.entities;

namespace Service.DependencyInjection
{
    public static class Container
    {


        public static void Services(this IServiceCollection services, IConfiguration configuration)
        {

            //add DbContext
            
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");

            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString));  

            services.AddScoped<AppDbContext, AppDbContext>();


            // Register AutoMapper

            services.AddAutoMapper(typeof(profile).Assembly);

            // Register CurrentUserServices and HttpContextAccessor

            services.AddHttpContextAccessor();

            // Register Logging

            services.AddTransient(typeof(ILoggerService), typeof(LoggerService));


            //Register for Generic

            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped(typeof(IGenericRepositoryAsync<,>), typeof(GenericRepositoryAsync<,>));



            // Register for Non-Generic

            services.AddScoped(typeof(IDoctorRepositoryAsync), typeof(DoctorRepositoryAsync));
            services.AddScoped(typeof(IAppointmentRepositoryAsync), typeof(AppointmentRepositoryAsync));
            services.AddScoped(typeof(IPatientRepositoryAsync), typeof(PatientRepositoryAsync));
            services.AddScoped(typeof(IPrescriptionRepositoryAsync), typeof(PrescriptionRepositoryAsync));
            services.AddScoped(typeof(IBillRepositoryAsync), typeof(BillRepositoryAsync));

            services.AddScoped(typeof(IBaseServiceRepository<,>), typeof(BaseServiceRepository<,>));
            services.AddScoped(typeof(IBaseServiceRepositoryAsync<,>), typeof(BaseServiceRepositoryAsync<,>));

            services.AddScoped(typeof(GenericRepository<,>), typeof(BaseServiceRepository<,>));
            services.AddScoped(typeof(GenericRepositoryAsync<,>), typeof(BaseServiceRepositoryAsync<,>));
            services.AddScoped(typeof(BaseServiceRepositoryAsync<Appointment, int>), typeof(AppointmentRepositoryAsync));
            services.AddScoped(typeof(BaseServiceRepositoryAsync<Bill, int>), typeof(BillRepositoryAsync));
            services.AddScoped(typeof(BaseServiceRepositoryAsync<Patient, int>), typeof(PatientRepositoryAsync));
            services.AddScoped(typeof(BaseServiceRepositoryAsync<Doctor, int>), typeof(DoctorRepositoryAsync));
            services.AddScoped(typeof(BaseServiceRepositoryAsync<Prescription, int>), typeof(PrescriptionRepositoryAsync));



            // Register ICurrentUserService

            services.AddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));


            // Register Services
                       
            services.AddScoped(typeof(IDoctorService), typeof(DoctorService));
            services.AddScoped(typeof(IAppointmentService), typeof(AppointmentService));
            services.AddScoped(typeof(IPatientService), typeof(PateintService));
            services.AddScoped(typeof(IPrescriptionService), typeof(PrescriptionService));
            services.AddScoped(typeof(IBillService), typeof(BillService));

            

            // Register UnitOfWork

            services.AddScoped(typeof(IunitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IunitOfWorkAsync), typeof(UnitOfWorkAsync));


        }
    }
}


