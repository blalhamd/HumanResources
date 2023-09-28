
using Framework.core.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Core.IServices;
using Service.Core.Models.ViewModels.Bill;
using Service.Entities.entities;

namespace Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;

        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        [HttpGet]
        public async Task<IQueryable<BillViewModel>> GetAllAsync()
        {
            var query = await _billService.GetAllAsync();

            return query;
        }

        [HttpGet("{id}")]
        public async Task<BillViewModel> GetByIdAsync(int id)
        {
            var query = await _billService.GetByIdAsync(id);

            return query;
        }


        [HttpPost]
        public async Task<BillViewModel> AddAsync(BillViewModel BillViewModel)
        {
            var search = await _billService.AddAsync(BillViewModel);

            return BillViewModel;
        }

        [HttpPost("Range")]
        public async Task<IList<BillViewModel>> AddRangeAsync(IEnumerable<BillViewModel> BillViewModels)
        {
            var query = await _billService.AddRangeAsync(BillViewModels);

            return query;
        }

        [HttpPut]
        public async Task<Bill> UpdateAsync(UpdateBillViewModel BillViewModel)
        {

            var query = await _billService.UpdateAsync(BillViewModel);

            return query;
        }

        [HttpPut("Range")]
        public async Task<IList<Bill>> UpdateRangeAsync(IEnumerable<UpdateBillViewModel> BillViewModels)
        {
            var query= await _billService.UpdateRangeAsync(BillViewModels);

            return query;
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await _billService.DeleteAsync(id);
        }

        [HttpGet("Search")]
        public async Task<GenericResult<IList<BillLightViewModel>>> Search(BillSearchViewModel searchViewModel)
        {
            var query = await _billService.Search(searchViewModel);

            return query;
        }

        [HttpGet("lookup/Search")]
        public async Task<GenericResult<IList<BillLookUpViewModel>>> SearchLookUp(BillLookUpSearchViewModel SearchModel)
        {
            var query = await _billService.SearchLookUp(SearchModel);

            return query;
        }

        [HttpGet("View/{id}")]
        public async Task<BillViewViewModel> Search(int id)
        {
            var query = await _billService.Search(id);

            return query;
        }


    }
}
