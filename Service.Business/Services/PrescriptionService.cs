
using AutoMapper;
using Framework.comman.Exceptions;
using Framework.core.comman;
using Framework.core.IRepositories;
using Framework.core.Models;
using Serilog;
using Service.Business.Services.Base;
using Service.Core.IRepositories;
using Service.Core.IServices;
using Service.Core.Models.ViewModels.Prescription;
using Service.DataAccess.Repositories;
using Service.Entities.entities;

namespace Service.Business.Services
{
    public class PrescriptionService : IBaseService, IPrescriptionService
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPrescriptionRepositoryAsync _PrescriptionRepositoryAsync;
        private readonly IunitOfWorkAsync _unitOfWorkAsync;
        private readonly ILoggerService _loggerService;
        public PrescriptionService(IMapper mapper, ICurrentUserService currentUserService, IPrescriptionRepositoryAsync prescriptionRepositoryAsync, IunitOfWorkAsync unitOfWorkAsync, ILoggerService loggerService)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
            _PrescriptionRepositoryAsync = prescriptionRepositoryAsync;
            _unitOfWorkAsync = unitOfWorkAsync;
            _loggerService = loggerService;
        }

        public async Task<IQueryable<PrescriptionViewModel>> GetAllAsync()
        {
            _loggerService.logInf($"Exceuting Get All Prescription Action...");

            var query = await _PrescriptionRepositoryAsync.GetAllAsync();

            if (query is null)
            {
                _loggerService.logError($"Not Found items");

                throw new ItemNotFoundException("Not found items");
            }

            var map = _mapper.Map<IList<PrescriptionViewModel>>(query);

            _loggerService.logInf($"items returned successfully");

            return map.AsQueryable();
        }

        public async Task<PrescriptionViewModel> GetByIdAsync(int id)
        {
            _loggerService.logInf($"Exceuting Get By Id Prescription Action...");

            var query = await _PrescriptionRepositoryAsync.GetByIdAsync(id);

            if (query is null)
            {
                _loggerService.logError($"Not found Items");

                throw new ItemNotFoundException("Not found items");
            }

            var map = _mapper.Map<PrescriptionViewModel>(query);

            _loggerService.logInf($"item returned successfully");

            return map;
        }

        public async Task<PrescriptionViewModel> AddAsync(PrescriptionViewModel PrescriptionViewModel)
        {
            var search = await _PrescriptionRepositoryAsync.FirstOrDefaultAsync(x => x.frequency== PrescriptionViewModel.frequency && x.AppointmentId== PrescriptionViewModel.AppointmentId && x.dosage== PrescriptionViewModel.dosage && x.duration== PrescriptionViewModel.duration && x.medication== PrescriptionViewModel.medication);

            if (search != null)
            {
                _loggerService.logError($"this item with ID {search.Id} is exist");
                throw new ItemAlreadyExistException($"this item with ID {search.Id} is exist");
            }

            var map = _mapper.Map<Prescription>(PrescriptionViewModel);

            await _PrescriptionRepositoryAsync.AddEntityAsync(map);
            await _unitOfWorkAsync.commitAsync();

            _loggerService.logInf("the Item Added Successfully");

            return PrescriptionViewModel;
        }

        public async Task<IList<PrescriptionViewModel>> AddRangeAsync(IEnumerable<PrescriptionViewModel> PrescriptionViewModels)
        {
            IList<Prescription> Prescriptions = new List<Prescription>();

            foreach (var item in PrescriptionViewModels)
            {
                var search = await _PrescriptionRepositoryAsync.FirstOrDefaultAsync(x => x.frequency == item.frequency && x.AppointmentId == item.AppointmentId && x.dosage == item.dosage && x.duration == item.duration && x.medication == item.medication);

                if (search != null)
                {
                    _loggerService.logError($"this item with ID {search.Id} is exist");

                    throw new ItemAlreadyExistException($"this item with ID {search.Id} is exist");
                }

                var map = _mapper.Map<Prescription>(item);
                Prescriptions.Add(map);
            }

            await _PrescriptionRepositoryAsync.AddRangeAsync(Prescriptions);
            await _unitOfWorkAsync.commitAsync();

            _loggerService.logInf("the Items Added Successfully");


            return (IList<PrescriptionViewModel>)PrescriptionViewModels;
        }

        public async Task<Prescription> UpdateAsync(UpdatePrescriptionViewModel PrescriptionViewModel)
        {
            var search = await _PrescriptionRepositoryAsync.FirstOrDefaultAsync(x => x.Id == PrescriptionViewModel.Id);

            if (search is null)
            {
                _loggerService.logError($"Not found Items");

                throw new ItemNotFoundException("Not found items");
            }

            search.frequency = PrescriptionViewModel.frequency;
            search.dosage = PrescriptionViewModel.dosage;
            search.AppointmentId = PrescriptionViewModel.AppointmentId;
            search.medication = PrescriptionViewModel.medication;
            search.duration = PrescriptionViewModel.duration;

            await _PrescriptionRepositoryAsync.UpdateEntityAsync(search);
            await _unitOfWorkAsync.commitAsync();

            _loggerService.logInf($"Item Updated Successfully");

            return search;
        }


        public async Task<IList<Prescription>> UpdateRangeAsync(IEnumerable<UpdatePrescriptionViewModel> PrescriptionViewModels)
        {
            IList<Prescription> Prescriptions = new List<Prescription>();

            foreach (var item in PrescriptionViewModels)
            {
                var search = await _PrescriptionRepositoryAsync.FirstOrDefaultAsync(x => x.Id == item.Id);

                if (search != null)
                {
                    _loggerService.logError($"this item with ID {search.Id} is exist");

                    throw new ItemAlreadyExistException($"this item with ID {search.Id} is exist");
                }

                search.frequency = item.frequency;
                search.dosage = item.dosage;
                search.AppointmentId = item.AppointmentId;
                search.medication = item.medication;
                search.duration = item.duration;

                Prescriptions.Add(search);
            }

            await _PrescriptionRepositoryAsync.UpdateRangeAsync(Prescriptions);
            await _unitOfWorkAsync.commitAsync();
            _loggerService.logInf($"Item Updated Successfully");

            return Prescriptions;
        }


        public async Task DeleteAsync(int id)
        {
            _loggerService.logInf($"Delete {id}");

            await _PrescriptionRepositoryAsync.DeleteAsync(id);
            await _unitOfWorkAsync.commitAsync();

            _loggerService.logInf($"Item Deleted Succsessfully");

        }

       
        public async Task<GenericResult<IList<PrescriptionLightViewModel>>> Search(PrescriptionSearchViewModel searchModel)
        {
            var query = await _PrescriptionRepositoryAsync.GetAllAsync();

            if (searchModel.Id != null)
            {
                query = query.Where(x => x.Id == searchModel.Id);
            }

            if (searchModel.AppointmentId != null)
            {
                query = query.Where(x => x.AppointmentId == searchModel.AppointmentId);
            }

            searchModel.pagination = await _PrescriptionRepositoryAsync.SetPaginationCountAsync(query, searchModel.pagination);

            var pagination = await _PrescriptionRepositoryAsync.SetPaginationAsync(query, searchModel.pagination);
            var sorting = await _PrescriptionRepositoryAsync.OrderSortAsync(query, searchModel.Sorting);

            var result = query.Select(x=> new PrescriptionLightViewModel()
            {
                Id = x.Id,
                AppointmentId = x.AppointmentId,
                dosage = x.dosage,
                duration = x.duration ,
                frequency = x.frequency ,
                medication = x.medication 

            }).ToList();

            var generic = new GenericResult<IList<PrescriptionLightViewModel>>()
            {
                pagination = searchModel.pagination,
                collection = result
            };

            return generic;
        }

        
        public async Task<GenericResult<IList<PrescriptionLookUpViewModel>>> SearchLookUp(PrescriptionLookUpSearchViewModel searchModel)
        {
            var query = await _PrescriptionRepositoryAsync.GetAllAsync();

            if (searchModel.Id != null)
            {
                query = query.Where(x => x.Id == searchModel.Id);
            }

            if (searchModel.AppointmentId != null)
            {
                query = query.Where(x => x.AppointmentId == searchModel.AppointmentId);
            }

            searchModel.pagination = await _PrescriptionRepositoryAsync.SetPaginationCountAsync(query, searchModel.pagination);

            var pagination = await _PrescriptionRepositoryAsync.SetPaginationAsync(query, searchModel.pagination);
            var sorting = await _PrescriptionRepositoryAsync.OrderSortAsync(query, searchModel.Sorting);

            var result = query.Select(x=> new PrescriptionLookUpViewModel()
            {
                Id = x.Id,
                AppointmentId = x.AppointmentId,
                dosage = x.dosage,
                duration = x.duration ,
                frequency = x.frequency ,
                medication = x.medication

            }).ToList();

            var generic = new GenericResult<IList<PrescriptionLookUpViewModel>>()
            {
                pagination = searchModel.pagination,
                collection = result
            };

            return generic;
        }

        public async Task<PrescriptionViewViewModel> Search(int id)
        {
            var query = await _PrescriptionRepositoryAsync.GetAllAsync();

            if (id == null)
            {
                _loggerService.logError($"the result is Null");

                throw new ArgumentNullException("id is null");
            }

            var entity = query.Where(x => x.Id == id).Select(x=> new PrescriptionViewViewModel()
            {
                AppointmentId= x.AppointmentId,
                dosage = x.dosage,
                duration = x.duration ,
                frequency = x.frequency ,
                medication = x.medication 
                
            }).FirstOrDefault();


            return entity;
        }
    }

}
