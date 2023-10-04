using Framework.core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Core.IServices;
using Service.Core.Models.ViewModels.Prescription;
using Service.Entities.entities;

namespace Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _PrescriptionService;

        public PrescriptionController(IPrescriptionService PrescriptionService)
        {
            _PrescriptionService = PrescriptionService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IQueryable<PrescriptionViewModel>>> GetAllAsync()
        {
            var query = await _PrescriptionService.GetAllAsync();

            return Ok(query);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<PrescriptionViewModel>> GetByIdAsync(int id)
        {
            var query = await _PrescriptionService.GetByIdAsync(id);

            return Ok(query);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PrescriptionViewModel>> AddAsync(PrescriptionViewModel PrescriptionViewModel)
        {
            var search = await _PrescriptionService.AddAsync(PrescriptionViewModel);

            return Created(" ",PrescriptionViewModel);
        }

        [HttpPost("Range")]
        [Authorize]
        public async Task<ActionResult<IList<PrescriptionViewModel>>> AddRangeAsync(IEnumerable<PrescriptionViewModel> PrescriptionViewModels)
        {
            var result = await _PrescriptionService.AddRangeAsync(PrescriptionViewModels);

            return Created(" ",result);
        }

        [HttpPut("{id})]
        [Authorize]
        public async Task<ActionResult<Prescription>> UpdateAsync(UpdatePrescriptionViewModel PrescriptionViewModel)
        {

            var result = await _PrescriptionService.UpdateAsync(PrescriptionViewModel);

            return Ok(result);
        }

        [HttpPut("Range")]
        [Authorize]
        public async Task<ActionResult<IList<Prescription>>> UpdateRangeAsync(IEnumerable<UpdatePrescriptionViewModel> PrescriptionViewModels)
        {
            var result = await _PrescriptionService.UpdateRangeAsync(PrescriptionViewModels);

            return Ok(result);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _PrescriptionService.DeleteAsync(id);

            return Ok();
        }

        [HttpGet("Search")]
        [Authorize]
        public async Task<ActionResult<GenericResult<IList<PrescriptionLightViewModel>>>> Search(PrescriptionSearchViewModel searchViewModel)
        {
            var query = await _PrescriptionService.Search(searchViewModel);

            return Ok(query);
        }

        [HttpGet("lookup/Search")]
        [Authorize]
        public async Task<ActionResult<GenericResult<IList<PrescriptionLookUpViewModel>>>> SearchLookUp(PrescriptionLookUpSearchViewModel SearchModel)
        {
            var query = await _PrescriptionService.SearchLookUp(SearchModel);

            return Ok(query);
        }

        [HttpGet("View/{id}")]
        [Authorize]
        public async Task<ActionResult<PrescriptionViewViewModel>> Search(int id)
        {
            var query = await _PrescriptionService.Search(id);

            return Ok(query);
        }

    }
}
