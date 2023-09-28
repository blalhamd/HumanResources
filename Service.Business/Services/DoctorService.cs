

using AutoMapper;
using Framework.comman.Exceptions;
using Framework.core.comman;
using Framework.core.IRepositories;
using Framework.core.Models;
using Service.Business.Services.Base;
using Service.Core.IRepositories;
using Service.Core.IServices;
using Service.Core.Models.ViewModels.Doctor;
using Service.Entities.entities;

namespace Service.Business.Services
{
    public class DoctorService : IBaseService, IDoctorService
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDoctorRepositoryAsync _DoctorRepositoryAsync;
        private readonly IunitOfWorkAsync _unitOfWorkAsync;
        private readonly ILoggerService _logger;

        public DoctorService(IMapper mapper, ICurrentUserService currentUserService, IDoctorRepositoryAsync doctorRepositoryAsync, IunitOfWorkAsync unitOfWorkAsync, ILoggerService logger)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
            _DoctorRepositoryAsync = doctorRepositoryAsync;
            _unitOfWorkAsync = unitOfWorkAsync;
            _logger = logger;
        }

        public async Task<IQueryable<DoctorViewModel>> GetAllAsync()
        {
            _logger.logInf("Executing Action Get All...");

            var query = await _DoctorRepositoryAsync.GetAllAsync();

            if (query is null)
            {
                _logger.logError("itrm Not found");
                throw new ItemNotFoundException("Not found items");
            }

            var map = _mapper.Map<IList<DoctorViewModel>>(query);

            _logger.logInf("Items returned successfully");

            return map.AsQueryable();
        }

        public async Task<DoctorViewModel> GetByIdAsync(int id)
        {
            _logger.logInf($"Executing Action Get By Id :{id}...");

            var query = await _DoctorRepositoryAsync.GetByIdAsync(id);

            if (query is null)
            {
                _logger.logError($"item with {id} is not found");
                throw new ItemNotFoundException("Not found items");
            }

            var map = _mapper.Map<DoctorViewModel>(query);

            _logger.logInf("item returned successfully");

            return map;
        }

        public async Task<DoctorViewModel> AddAsync(DoctorViewModel DoctorViewModel)
        {
            var search = await _DoctorRepositoryAsync.FirstOrDefaultAsync(x => x.license == DoctorViewModel.license && x.contact== DoctorViewModel.contact && x.specialty == DoctorViewModel.specialty && x.Name== DoctorViewModel.Name);

            if (search != null)
            {
                _logger.logError($"this item with ID {search.Id} is exist");
                throw new ItemAlreadyExistException($"this item with ID {search.Id} is exist");
            }

            var map = _mapper.Map<Doctor>(DoctorViewModel);

            await _DoctorRepositoryAsync.AddEntityAsync(map);
            await _unitOfWorkAsync.commitAsync();

            _logger.logInf("item added successfully");

            return DoctorViewModel;
        }

        public async Task<IList<DoctorViewModel>> AddRangeAsync(IEnumerable<DoctorViewModel> DoctorViewModels)
        {
            IList<Doctor> Doctors = new List<Doctor>();

            foreach (var item in DoctorViewModels)
            {
                var search = await _DoctorRepositoryAsync.FirstOrDefaultAsync(x => x.license == item.license && x.contact == item.contact && x.specialty == item.specialty && x.Name == item.Name);

                if (search != null)
                {
                    _logger.logError($"this item with ID {search.Id} is exist");

                    throw new ItemAlreadyExistException($"this item with ID {search.Id} is exist");
                }

                var map = _mapper.Map<Doctor>(item);
                Doctors.Add(map);
            }

            await _DoctorRepositoryAsync.AddRangeAsync(Doctors);
            await _unitOfWorkAsync.commitAsync();
            _logger.logInf("item added successfully");

            return (IList<DoctorViewModel>)DoctorViewModels;
        }

        public async Task<Doctor> UpdateAsync(UpdateDoctorViewModel DoctorViewModel)
        {
            _logger.logInf($"Exceuting update Doctor with ID: {DoctorViewModel.Id}...");
            var search = await _DoctorRepositoryAsync.GetByIdAsync(DoctorViewModel.Id);

            if (search is null)
            {
                _logger.logInf("Item Not found");

                throw new ItemNotFoundException("Not found items");
            }

            search.contact = DoctorViewModel.contact;
            search.specialty = DoctorViewModel.specialty;
            search.specialty = DoctorViewModel.specialty;
            search.license = DoctorViewModel.license;

            await _DoctorRepositoryAsync.UpdateEntityAsync(search);
            await _unitOfWorkAsync.commitAsync();

            _logger.logInf("Item Updated successfully");

            return search;
        }


        public async Task<IList<Doctor>> UpdateRangeAsync(IEnumerable<UpdateDoctorViewModel> DoctorViewModels)
        {
            IList<Doctor> Doctors = new List<Doctor>();

            foreach (var item in DoctorViewModels)
            {
                var search = await _DoctorRepositoryAsync.FirstOrDefaultAsync(x => x.Id == item.Id);

                if (search != null)
                {
                    _logger.logWarning("Item aleardy exist found");

                    throw new ItemAlreadyExistException($"this item with ID {search.Id} is exist");
                }

                search.contact = item.contact;
                search.specialty = item.specialty;
                search.specialty = item.specialty;
                search.license = item.license;

                Doctors.Add(search);
            }

            await _DoctorRepositoryAsync.UpdateRangeAsync(Doctors);
            await _unitOfWorkAsync.commitAsync();

            _logger.logInf("Items Updated successfully");

            return Doctors;
        }


        public async Task DeleteAsync(int id)
        {
            _logger.logInf($"Delete {id}");

            await _DoctorRepositoryAsync.DeleteAsync(id);
            await _unitOfWorkAsync.commitAsync();

            _logger.logInf("Item Deleted successfully");

        }

        
        public async Task<GenericResult<IList<DoctorLightViewModel>>> Search(DoctorSearchViewModel searchModel)
        {
            var query = await _DoctorRepositoryAsync.GetAllAsync();

            if(searchModel.Id != null)
            {
                query = query.Where(x => x.Id == searchModel.Id);
            }

            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                query = query.Where(x => x.Name == searchModel.Name);
            }

            searchModel.pagination = await _DoctorRepositoryAsync.SetPaginationCountAsync(query, searchModel.pagination);

            var pagination = await _DoctorRepositoryAsync.SetPaginationAsync(query, searchModel.pagination);
            var sorting = await _DoctorRepositoryAsync.OrderSortAsync(query, searchModel.Sorting);

            var result = query.Select(x=> new DoctorLightViewModel()
            {
                Id = x.Id,
                contact = x.contact,
                Name= x.Name,
                license= x.license,
                specialty = x.specialty
            }).ToList();

            var generic = new GenericResult<IList<DoctorLightViewModel>>()
            {
                pagination = searchModel.pagination,
                collection = result
            };

            return generic;
        }

    
        public async Task<GenericResult<IList<DoctorLookUpViewModel>>> SearchLookUp(DoctorLookUpSearchViewModel searchModel)
        {
            var query = await _DoctorRepositoryAsync.GetAllAsync();

            if (searchModel.Id != null)
            {
                query = query.Where(x => x.Id == searchModel.Id);
            }

            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                query = query.Where(x => x.Name == searchModel.Name);
            }

            searchModel.pagination = await _DoctorRepositoryAsync.SetPaginationCountAsync(query, searchModel.pagination);

            var pagination = await _DoctorRepositoryAsync.SetPaginationAsync(query, searchModel.pagination);
            var sorting = await _DoctorRepositoryAsync.OrderSortAsync(query, searchModel.Sorting);

            var result = query.Select(x=> new DoctorLookUpViewModel()
            {
               
                Id = x.Id,
                contact = x.contact,
                license= x.license,
                Name= x.Name,
                specialty = x.specialty

            }).ToList();

            var generic = new GenericResult<IList<DoctorLookUpViewModel>>()
            {
                pagination = searchModel.pagination,
                collection = result
            };

            return generic;
        }

        public async Task<DoctorViewViewModel> Search(int id)
        {
            var query = await _DoctorRepositoryAsync.GetAllAsync();

            if (id == null)
            {
                throw new ArgumentNullException("id is null");
            }

            var entity = query.Where(x => x.Id == id).Select(x=> new DoctorViewViewModel()
            {
                contact= x.contact,
                license = x.license,
                Name = x.Name,
                specialty = x.specialty

            })
              .FirstOrDefault();


            return entity;
        }

    }
}