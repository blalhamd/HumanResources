
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
        [Authorize]
        public async Task<ActionResult<IQueryable<BillViewModel>>> GetAllAsync()
        {
            var query = await _billService.GetAllAsync();

            return Ok(query);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<BillViewModel>> GetByIdAsync(int id)
        {
            var query = await _billService.GetByIdAsync(id);

            return Ok(query);
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BillViewModel>> AddAsync(BillViewModel BillViewModel)
        {
            var search = await _billService.AddAsync(BillViewModel);

            return Created(" ",BillViewModel);
        }

        [HttpPost("Range")]
        [Authorize]
        public async Task<ActionResult<IList<BillViewModel>>> AddRangeAsync(IEnumerable<BillViewModel> BillViewModels)
        {
            var result = await _billService.AddRangeAsync(BillViewModels);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Bill>> UpdateAsync(UpdateBillViewModel BillViewModel)
        {

            var result = await _billService.UpdateAsync(BillViewModel);

            return Ok(result);
        }

        [HttpPut("Range")]
        [Authorize]
        public async Task<ActionResult<IList<Bill>>> UpdateRangeAsync(IEnumerable<UpdateBillViewModel> BillViewModels)
        {
            var result= await _billService.UpdateRangeAsync(BillViewModels);

            return Ok(query);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task DeleteAsync(int id)
        {
            await _billService.DeleteAsync(id);
        }

        [HttpGet("Search")]
        [Authorize]
        public async Task<ActionResult<GenericResult<IList<BillLightViewModel>>>> Search(BillSearchViewModel searchViewModel)
        {
            var query = await _billService.Search(searchViewModel);

            return Ok(query);
        }

        [HttpGet("lookup/Search")]
        [Authorize]
        public async Task<ActionResult<GenericResult<IList<BillLookUpViewModel>>>> SearchLookUp(BillLookUpSearchViewModel SearchModel)
        {
            var query = await _billService.SearchLookUp(SearchModel);

            return Ok(query);
        }

        [HttpGet("View/{id}")]
        [Authorize]
        public async Task<ActionResult<BillViewViewModel>> Search(int id)
        {
            var query = await _billService.Search(id);

            return Ok(query);
        }


    }
}
