

using AutoMapper;
using Service.Core.Models.DTOs;
using Service.Core.Models.ViewModels.Appointment;
using Service.Core.Models.ViewModels.Bill;
using Service.Core.Models.ViewModels.Doctor;
using Service.Core.Models.ViewModels.Patient;
using Service.Core.Models.ViewModels.Prescription;
using Service.Entities.entities;

namespace Service.Core.AutoMapper
{
    public class profile : Profile
    {
        public profile()
        {
            CreateMap<Bill, BillViewModel>().ReverseMap();  
            CreateMap<Appointment, AppointmentViewModel>().ReverseMap();  
            CreateMap<Doctor, DoctorViewModel>().ReverseMap();  
            CreateMap<Patient, PatientViewModel>().ReverseMap();  
            CreateMap<Prescription, PrescriptionViewModel>().ReverseMap(); 
            CreateMap<Patient,PatientDTO>().ReverseMap();
            CreateMap<Doctor,DoctorDTO>().ReverseMap();
            CreateMap<Appointment,AppointmentDTO>().ReverseMap();

        }
    } 
}
