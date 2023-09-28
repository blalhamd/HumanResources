

using AutoMapper;
using Framework.comman.Exceptions;
using Framework.core.comman;
using Framework.core.IRepositories;
using Framework.core.Models;
using Service.Business.Services.Base;
using Service.Core.IRepositories;
using Service.Core.IServices;
using Service.Core.Models.ViewModels.Patient;
using Service.Entities.entities;

namespace Service.Business.Services
{
    public class PateintService : IBaseService, IPatientService
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPatientRepositoryAsync _PatientRepositoryAsync;
        private readonly IunitOfWorkAsync _unitOfWorkAsync;
        private readonly ILoggerService _loggerService;

        public PateintService(IMapper mapper, ICurrentUserService currentUserService, IPatientRepositoryAsync patientRepositoryAsync, IunitOfWorkAsync unitOfWorkAsync, ILoggerService loggerService)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
            _PatientRepositoryAsync = patientRepositoryAsync;
            _unitOfWorkAsync = unitOfWorkAsync;
            _loggerService = loggerService;
        }

        public async Task<IQueryable<PatientViewModel>> GetAllAsync()
        {
            var query = await _PatientRepositoryAsync.GetAllAsync();

            if (query is null)
            {
                _loggerService.logError($"Not found items");

                throw new ItemNotFoundException("Not found items");
            }

            var map = _mapper.Map<IList<PatientViewModel>>(query);

            _loggerService.logInf("Items returned successfully");

            return map.AsQueryable();
        }

        public async Task<PatientViewModel> GetByIdAsync(int id)
        {
            var query = await _PatientRepositoryAsync.GetByIdAsync(id);

            if (query is null)
            {
                _loggerService.logError($"Not found items");

                throw new ItemNotFoundException("Not found items");
            }

            var map = _mapper.Map<PatientViewModel>(query);

            _loggerService.logInf("Item Returned successfuly");

            return map;
        }

        public async Task<PatientViewModel> AddAsync(PatientViewModel PatientViewModel)
        {
            var search = await _PatientRepositoryAsync.FirstOrDefaultAsync(x => x.HasInsurance == PatientViewModel.HasInsurance && x.gender== PatientViewModel.gender && x.address==PatientViewModel.address && x.dateOfBirth == PatientViewModel.dateOfBirth && x.Name ==PatientViewModel.Name);

            if (search != null)
            {
                _loggerService.logWarning($"this item with ID {search.Id} is exist");
                throw new DataDuplicationException($"this item with ID {search.Id} is exist");
            }

            var map = _mapper.Map<Patient>(PatientViewModel);

            await _PatientRepositoryAsync.AddEntityAsync(map);
            await _unitOfWorkAsync.commitAsync();

            _loggerService.logInf("Item added successfully");

            return PatientViewModel;
        }

        public async Task<IList<PatientViewModel>> AddRangeAsync(IEnumerable<PatientViewModel> PatientViewModels)
        {
            IList<Patient> Patients = new List<Patient>();

            foreach (var item in PatientViewModels)
            {
                var search = await _PatientRepositoryAsync.FirstOrDefaultAsync(x => x.HasInsurance == item.HasInsurance && x.gender == item.gender && x.address == item.address && x.dateOfBirth == item.dateOfBirth  && x.Name == item.Name);

                if (search != null)
                {
                    _loggerService.logWarning($"this item with ID {search.Id} is exist");
                    throw new DataDuplicationException($"this item with ID {search.Id} is exist");
                }

                var map = _mapper.Map<Patient>(item);
                Patients.Add(map);
            }

            await _PatientRepositoryAsync.AddRangeAsync(Patients);
            await _unitOfWorkAsync.commitAsync();
            _loggerService.logInf("Items added successfully");


            return (IList<PatientViewModel>)PatientViewModels;
        }

        public async Task<Patient> UpdateAsync(UpdatePatientViewModel PatientViewModel)
        {
            var search = await _PatientRepositoryAsync.FirstOrDefaultAsync(x => x.Id == PatientViewModel.Id);

            if (search is null)
            {
                _loggerService.logError($"Not found items");

                throw new ItemNotFoundException("Not found item");
            }

            search.dateOfBirth = PatientViewModel.dateOfBirth;
            search.phone = PatientViewModel.phone;
            search.address = PatientViewModel.address;
            search.gender = PatientViewModel.gender;
            search.HasInsurance = PatientViewModel.HasInsurance;
            search.Name = PatientViewModel.Name;

            await _PatientRepositoryAsync.UpdateEntityAsync(search);
            await _unitOfWorkAsync.commitAsync();
           
            _loggerService.logInf("Item Updated successfuly");

            return search;
        }


        public async Task<IList<Patient>> UpdateRangeAsync(IEnumerable<UpdatePatientViewModel> PatientViewModels)
        {
            IList<Patient> Patients = new List<Patient>();

            foreach (var item in PatientViewModels)
            {
                var search = await _PatientRepositoryAsync.FirstOrDefaultAsync(x => x.Id == item.Id);

                if (search != null)
                {
                    _loggerService.logWarning($"this item with ID {search.Id} is exist");

                    throw new ItemAlreadyExistException($"this item with ID {search.Id} is exist");
                }

                search.dateOfBirth = item.dateOfBirth;
                search.phone = item.phone;
                search.address = item.address;
                search.gender = item.gender;
                search.HasInsurance = item.HasInsurance;

                Patients.Add(search);
            }

            await _PatientRepositoryAsync.UpdateRangeAsync(Patients);
            await _unitOfWorkAsync.commitAsync();
            _loggerService.logInf("Items Updated successfuly");

            return Patients;
        }

        public async Task DeleteAsync(int id)
        {
            _loggerService.logInf("Excecuting Delete Item Action");

            await _PatientRepositoryAsync.DeleteAsync(id);
            await _unitOfWorkAsync.commitAsync();

            _loggerService.logInf("Item Deleted successfuly");

        }

       
        public async Task<GenericResult<IList<PatientLightViewModel>>> Search(PatientSearchViewModel searchModel)
        {
            var query = await _PatientRepositoryAsync.GetAllAsync();

            if (searchModel.Id != null)
            {
                query = query.Where(x => x.Id == searchModel.Id);
            }

            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                query = query.Where(x => x.Name == searchModel.Name);
            }

            searchModel.pagination = await _PatientRepositoryAsync.SetPaginationCountAsync(query, searchModel.pagination);

            var pagination = await _PatientRepositoryAsync.SetPaginationAsync(query, searchModel.pagination);
            var sorting = await _PatientRepositoryAsync.OrderSortAsync(query, searchModel.Sorting);

            var result = query.Select(x=> new PatientLightViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                address = x.address,
                dateOfBirth= x.dateOfBirth,
                gender= x.gender,
                HasInsurance = x.HasInsurance ,
                phone = x.phone

            }).ToList();

            var generic = new GenericResult<IList<PatientLightViewModel>>()
            {
                pagination = searchModel.pagination,
                collection = result
            };

            return generic;
        }

     
        public async Task<GenericResult<IList<PatientLookUpViewModel>>> SearchLookUp(PatientLookUpSearchViewModel searchModel)
        {
            var query = await _PatientRepositoryAsync.GetAllAsync();

            if (searchModel.Id != null)
            {
                query = query.Where(x => x.Id == searchModel.Id);
            }

            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                query = query.Where(x => x.Name == searchModel.Name);
            }

            searchModel.pagination = await _PatientRepositoryAsync.SetPaginationCountAsync(query, searchModel.pagination);

            var pagination = await _PatientRepositoryAsync.SetPaginationAsync(query, searchModel.pagination);
            var sorting = await _PatientRepositoryAsync.OrderSortAsync(query, searchModel.Sorting);

            var result = query.Select(x=> new PatientLookUpViewModel()
            {
                Id = x.Id,
                address = x.address,
                dateOfBirth= x.dateOfBirth,
                gender= x.gender,
                HasInsurance= x.HasInsurance,
                Name = x.Name,
                phone = x.phone

            }).ToList();

            var generic = new GenericResult<IList<PatientLookUpViewModel>>()
            {
                pagination = searchModel.pagination,
                collection = result
            };

            return generic;
        }

        public async Task<PatientViewViewModel> Search(int id)
        {
            var query = await _PatientRepositoryAsync.GetAllAsync();

            if (id == null)
            {
                throw new ArgumentNullException("id is null");
            }

            var entity = query.Where(x => x.Id == id).Select(x=> new PatientViewViewModel()
            {
                Name = x.Name,
                address = x.address,
                dateOfBirth= x.dateOfBirth,
                gender = x.gender,
                HasInsurance= x.HasInsurance,
                phone = x.phone
            })
               .FirstOrDefault();


            return entity;
        }

    }
}
