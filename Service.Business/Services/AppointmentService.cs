
using AutoMapper;
using Framework.comman.Exceptions;
using Framework.core.comman;
using Framework.core.IRepositories;
using Framework.core.Models;
using Service.Business.Services.Base;
using Service.Core.IRepositories;
using Service.Core.IServices;
using Service.Core.Models.ViewModels.Appointment;
using Service.Entities.entities;

namespace Service.Business.Services
{
    public class AppointmentService : IBaseService, IAppointmentService
    {
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAppointmentRepositoryAsync _appointmentRepositoryAsync;
        private readonly IunitOfWorkAsync _unitOfWorkAsync;

        public AppointmentService(IMapper mapper, ICurrentUserService currentUserService, IAppointmentRepositoryAsync appointmentRepositoryAsync, IunitOfWorkAsync unitOfWorkAsync, ILoggerService loggerService)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
            _appointmentRepositoryAsync = appointmentRepositoryAsync;
            _unitOfWorkAsync = unitOfWorkAsync;
            _loggerService = loggerService;
        }

        public async Task<IQueryable<AppointmentViewModel>> GetAllAsync()
        {
            var query = await _appointmentRepositoryAsync.GetAllAsync();

            if (query is null)
            {
                _loggerService.logError($"Items are not found");
                throw new ItemNotFoundException($"Items are not found");
            }

            var map = _mapper.Map<IList<AppointmentViewModel>>(query);

            _loggerService.logInf("Items returned successfully");

            return map.AsQueryable();
        }



        public async Task<AppointmentViewModel> GetByIdAsync(int id)
        {
            var query = await _appointmentRepositoryAsync.GetByIdAsync(id);

            if (query is null)
            {
                _loggerService.logError($"Item with ID {query.Id} is not found");

                throw new ItemNotFoundException($"Item with ID {query.Id} is not found");
            }

            var map = _mapper.Map<AppointmentViewModel>(query);

            _loggerService.logInf("Item returned successfully");

            return map;
        }



        public async Task<AppointmentViewModel> AddAsync(AppointmentViewModel appointmentViewModel)
        {
              _loggerService.logInf("Execution adding Appointment...");

            var search = await _appointmentRepositoryAsync.FirstOrDefaultAsync(x => x.doctorId == appointmentViewModel.doctorId && x.patientId == appointmentViewModel.patientId && x.status == appointmentViewModel.status && appointmentViewModel.dateTime == appointmentViewModel.dateTime);

            if (search != null)
            {
                _loggerService.logError($"Item with ID {search.Id} already exists.");
                throw new DataDuplicationException($"Item with ID {search.Id} already exists.");

            }

            var map = _mapper.Map<Appointment>(appointmentViewModel);

            await _appointmentRepositoryAsync.AddEntityAsync(map);
            await _unitOfWorkAsync.commitAsync();
            
            _loggerService.logInf("Appointment added successfully.");

            return appointmentViewModel;
        }


        public async Task<IList<AppointmentViewModel>> AddRangeAsync(IEnumerable<AppointmentViewModel> appointmentViewModels)
        {
            IList<Appointment> appointments = new List<Appointment>();

            foreach (var item in appointmentViewModels)
            {
                var search = await _appointmentRepositoryAsync.FirstOrDefaultAsync(x=> x.doctorId==item.doctorId && x.patientId==item.patientId && x.status == item.status && item.dateTime== item.dateTime);
                
                if (search != null)
                {
                    _loggerService.logError($"Item with ID {search.Id} alraedy exist");
                    throw new DataDuplicationException($"Item with ID { search.Id } alraedy exist");
                }

                var map = _mapper.Map<Appointment>(item);
                appointments.Add(map);
            }

            await _appointmentRepositoryAsync.AddRangeAsync(appointments);
            await _unitOfWorkAsync.commitAsync();

            _loggerService.logInf("Item Added Successfully");

            return (IList<AppointmentViewModel>) appointmentViewModels;
        }


        public async Task<Appointment> UpdateAsync(UpdateAppointMentViewModel appointmentViewModel)
        {
            var search = await _appointmentRepositoryAsync.GetByIdAsync(appointmentViewModel.Id);

            if (search is null)
            {
                _loggerService.logError($"Item with ID {search.Id} is not found");
                throw new ItemNotFoundException($"Item with ID {search.Id} is not found");
            }

            search.patientId = appointmentViewModel.patientId;
            search.status = appointmentViewModel.status;
            search.dateTime = appointmentViewModel.dateTime;
            search.doctorId = appointmentViewModel.doctorId;

            await _appointmentRepositoryAsync.UpdateEntityAsync(search);
            await _unitOfWorkAsync.commitAsync();
            _loggerService.logInf("Item updated successfully");

            return search;
        }


        public async Task<IList<Appointment>> UpdateRangeAsync(IEnumerable<UpdateAppointMentViewModel> appointmentViewModels)
        {
            IList<Appointment> appointments = new List<Appointment>();

            foreach (var item in appointmentViewModels)
            {
                var search = await _appointmentRepositoryAsync.FirstOrDefaultAsync(x => x.Id == item.Id);

                if (search == null)
                {
                    _loggerService.logError($"Item with ID {search.Id} is not found");

                    throw new ItemNotFoundException($"Item with ID {item.Id} is not found");
                }

                search.patientId = item.patientId;
                search.status = item.status;
                search.dateTime = item.dateTime;
                search.doctorId = item.doctorId;

                appointments.Add(search);
            }

            await _appointmentRepositoryAsync.UpdateRangeAsync(appointments);
            await _unitOfWorkAsync.commitAsync();

            _loggerService.logInf("Items updated successfully");

            return appointments;
        }


        public async Task DeleteAsync(int id)
        {
           _loggerService.logInf($"Executing Delete action for Item with Id {id}");

            await _appointmentRepositoryAsync.DeleteAsync(id);
            await _unitOfWorkAsync.commitAsync();

        }


        public async Task<GenericResult<IList<AppointmentLightViewModel>>> Search(AppointmentSearchViewModel searchModel)
        {
            var query = await _appointmentRepositoryAsync.GetAllAsync();

            if(searchModel.Id != null)
            {
                query = query.Where(x => x.Id == searchModel.Id);
            }

            if(searchModel.doctorId != null)
            {
                query = query.Where(x => x.doctorId == searchModel.doctorId);
            }

            if (searchModel.patientId != null)
            {
                query = query.Where(x => x.patientId == searchModel.patientId);
            }

            searchModel.pagination = await _appointmentRepositoryAsync.SetPaginationCountAsync(query,searchModel.pagination);

            query = await _appointmentRepositoryAsync.OrderSortAsync(query,searchModel.Sorting);
            query = await _appointmentRepositoryAsync.SetPaginationAsync(query,searchModel.pagination);

            var result = query.Select(x=> new AppointmentLightViewModel()
            {
                Id = x.Id,
                doctorId= x.doctorId,
                patientId = x.patientId,
                status = x.status
                
            }).ToList();

            var generic = new GenericResult<IList<AppointmentLightViewModel>>()
            {
                pagination = searchModel.pagination,
                collection = result
            };

            return generic;
        }


        public async Task<GenericResult<IList<AppointmentLookUpViewModel>>> SearchLookUp(AppointmentLookUpSearchViewModel searchModel)
        {
            var query = await _appointmentRepositoryAsync.GetAllAsync();

            if(searchModel.Id != null)
            {
                query = query.Where(x => x.Id == searchModel.Id);
            }

            if(searchModel.doctorId != null)
            {
                query = query.Where(x => x.doctorId == searchModel.doctorId);
            }

            if (searchModel.patientId != null)
            {
                query = query.Where(x => x.patientId == searchModel.patientId);
            }

            searchModel.pagination = await _appointmentRepositoryAsync.SetPaginationCountAsync(query, searchModel.pagination);

            query = await _appointmentRepositoryAsync.OrderSortAsync(query, searchModel.Sorting);
            query = await _appointmentRepositoryAsync.SetPaginationAsync(query, searchModel.pagination);

            var result = query.Select(x => new AppointmentLookUpViewModel()
            {
                Id = x.Id,
                dateTime = x.dateTime,
                doctorId = x.doctorId,
                patientId = x.patientId,
                status = x.status
            }).ToList();

            var genericResult = new GenericResult<IList<AppointmentLookUpViewModel>>()
            {
                pagination = searchModel.pagination,
                collection = result
            };


            return genericResult;
        }


        public async Task<AppointmentViewViewModel> Search(int id)
        {
            var query = await _appointmentRepositoryAsync.GetAllAsync();

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = query.Where(x => x.Id == id)
                   .Select(x => new AppointmentViewViewModel()
                   {
                       status = x.status,
                       patientId = x.patientId,
                       dateTime = x.dateTime,
                       doctorId = x.doctorId
                   })
                   .FirstOrDefault();


            return result;
        }


    }
}

