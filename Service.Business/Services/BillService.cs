
using AutoMapper;
using Framework.comman.Exceptions;
using Framework.core.comman;
using Framework.core.IRepositories;
using Framework.core.Models;
using Service.Business.Services.Base;
using Service.Core.IRepositories;
using Service.Core.IServices;
using Service.Core.Models.ViewModels.Bill;
using Service.Entities.entities;

namespace Service.Business.Services
{
    public class BillService : IBaseService, IBillService
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IBillRepositoryAsync _BillRepositoryAsync;
        private readonly IunitOfWorkAsync _unitOfWorkAsync;
        private readonly ILoggerService _logger;

        public BillService(IMapper mapper, ICurrentUserService currentUserService, IBillRepositoryAsync billRepositoryAsync, IunitOfWorkAsync unitOfWorkAsync, ILoggerService logger)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
            _BillRepositoryAsync = billRepositoryAsync;
            _unitOfWorkAsync = unitOfWorkAsync;
            _logger = logger;
        }

        public async Task<IQueryable<BillViewModel>> GetAllAsync()
        {
            var query = await _BillRepositoryAsync.GetAllAsync();

            if (query is null)
            {
                _logger.logError("Not found elements");
                throw new ItemNotFoundException("Not found elements");
            }

            var map = _mapper.Map<IList<BillViewModel>>(query);

            _logger.logInf("Items Returned successfully");

            return map.AsQueryable();
        }

        public async Task<BillViewModel> GetByIdAsync(int id)
        {
            var query = await _BillRepositoryAsync.GetByIdAsync(id);

            if (query is null)
            {
                _logger.logError("Not found elements");
                throw new ItemNotFoundException("Not found elements");
            }

            var map = _mapper.Map<BillViewModel>(query);

            _logger.logInf("Item Returned successfully");

            return map;
        }


        public async Task<BillViewModel> AddAsync(BillViewModel BillViewModel)
        {
            _logger.logInf("Exceuting Add action");

            var search = await _BillRepositoryAsync.FirstOrDefaultAsync(x => x.dateTime == BillViewModel.dateTime && x.amount == BillViewModel.amount && x.patientId == BillViewModel.patientId && x.status== BillViewModel.status);

            if (search != null)
            {
                _logger.logError($"item with this ID {search.Id} is exist");
                throw new ItemAlreadyExistException($"item with this ID {search.Id} is exist");
            }

            var map = _mapper.Map<Bill>(BillViewModel);

            await _BillRepositoryAsync.AddEntityAsync(map);
            await _unitOfWorkAsync.commitAsync();

            _logger.logInf("Item Added successfully");

            return BillViewModel;
        }

        public async Task<IList<BillViewModel>> AddRangeAsync(IEnumerable<BillViewModel> BillViewModels)
        {
            IList<Bill> Bills = new List<Bill>();

            foreach (var item in BillViewModels)
            {
                var search = await _BillRepositoryAsync.FirstOrDefaultAsync(x => x.dateTime == item.dateTime && x.amount == item.amount && x.patientId == item.patientId && x.status == item.status);

                if (search != null)
                {
                    _logger.logError($"item with this ID {search.Id} is exist");

                    throw new ItemAlreadyExistException($"item with this ID {search.Id} is exist");
                }

                var map = _mapper.Map<Bill>(item);
                Bills.Add(map);
            }

            await _BillRepositoryAsync.AddRangeAsync(Bills);
            await _unitOfWorkAsync.commitAsync();

            _logger.logInf("Items Added successfully");


            return (IList<BillViewModel>)BillViewModels;
        }

        public async Task<Bill> UpdateAsync(UpdateBillViewModel BillViewModel)
        {
            var search = await _BillRepositoryAsync.GetByIdAsync(BillViewModel.Id);

            if (search is null)
            {
                throw new ItemNotFoundException("Not found element");
            }

            search.status = BillViewModel.status;
            search.amount = BillViewModel.amount;
            search.dateTime = BillViewModel.dateTime;
            search.patientId = BillViewModel.patientId;

            await _BillRepositoryAsync.UpdateEntityAsync(search);
            await _unitOfWorkAsync.commitAsync();

            _logger.logInf("Item Updated successfully");

            return search;
        }

        public async Task<IList<Bill>> UpdateRangeAsync(IEnumerable<UpdateBillViewModel> BillViewModels)
        {
            IList<Bill> Bills = new List<Bill>();

            foreach (var item in BillViewModels)
            {
                var search = await _BillRepositoryAsync.FirstOrDefaultAsync(x => x.Id == item.Id);

                if (search != null)
                {
                    throw new ItemAlreadyExistException($"this item with ID {search.Id} is exist");
                }


                search.status = item.status;
                search.amount = item.amount;
                search.dateTime = item.dateTime;
                search.patientId = item.patientId;

                Bills.Add(search);
            }

            await _BillRepositoryAsync.UpdateRangeAsync(Bills);
            await _unitOfWorkAsync.commitAsync();
            _logger.logInf("Items Updated successfully");


            return Bills;
        }


        public async Task DeleteAsync(int id)
        {
            _logger.logInf($"Exceuting Delete Action");

            await _BillRepositoryAsync.DeleteAsync(id);
            await _unitOfWorkAsync.commitAsync();

            _logger.logInf($"item Deleted successfully");

        }

       
        public async Task<GenericResult<IList<BillLightViewModel>>> Search(BillSearchViewModel searchModel)
        {
            var query = await _BillRepositoryAsync.GetAllAsync();

            if(searchModel.Id != null)
            {
                query = query.Where(x => x.Id == searchModel.Id);
            }

            if (searchModel.patientId != null)
            {
                query = query.Where(x => x.patientId == searchModel.patientId);
            }

            searchModel.pagination = await _BillRepositoryAsync.SetPaginationCountAsync(query, searchModel.pagination);

            var pagination =await _BillRepositoryAsync.SetPaginationAsync(query, searchModel.pagination);
            var sorting = await _BillRepositoryAsync.OrderSortAsync(query, searchModel.Sorting);

            var result = query.Select(x=> new BillLightViewModel()
            {
                Id = x.Id,
                amount = x.amount,
                dateTime= x.dateTime,
                patientId= x.patientId,
                status = x.status
           
            }).ToList();

            var generic = new GenericResult<IList<BillLightViewModel>>()
            {
                pagination = searchModel.pagination,
                collection = result
            };

            return generic;
        }


        public async Task<GenericResult<IList<BillLookUpViewModel>>> SearchLookUp(BillLookUpSearchViewModel searchModel)
        {
            var query = await _BillRepositoryAsync.GetAllAsync();

            if (searchModel.Id != null)
            {
                query = query.Where(x => x.Id == searchModel.Id);
            }

            if (searchModel.patientId != null)
            {
                query = query.Where(x => x.patientId == searchModel.patientId);
            }

            searchModel.pagination = await _BillRepositoryAsync.SetPaginationCountAsync(query,searchModel.pagination);

            var pagination = await _BillRepositoryAsync.SetPaginationAsync(query, searchModel.pagination);
            var sorting = await _BillRepositoryAsync.OrderSortAsync(query, searchModel.Sorting);

            var result = query.Select(x => new BillLookUpViewModel()
            {
                Id = x.Id,
                amount = x.amount,
                dateTime = x.dateTime,
                patientId = x.patientId,
                status = x.status

            }).ToList();

            var generic = new GenericResult<IList<BillLookUpViewModel>>()
            {
                pagination = searchModel.pagination,
                collection = result
            };

            return generic;
        }

        public async Task<BillViewViewModel> Search(int id)
        {
            var query = await _BillRepositoryAsync.GetAllAsync();

            if (id == null)
            {
                throw new ArgumentNullException("id is null");
            }

            var entity = query.Where(x => x.Id == id).Select(x=> new BillViewViewModel()
            {
                 amount = x.amount,
                 dateTime= x.dateTime,
                 patientId= x.patientId,
                 status = x.status
            })
              .FirstOrDefault();

            return entity;
        }


    }
}