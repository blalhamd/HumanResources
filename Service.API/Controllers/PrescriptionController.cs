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
        public async Task<IQueryable<PrescriptionViewModel>> GetAllAsync()
        {
            var query = await _PrescriptionService.GetAllAsync();

            return query;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<PrescriptionViewModel> GetByIdAsync(int id)
        {
            var query = await _PrescriptionService.GetByIdAsync(id);

            return query;
        }

        [HttpPost]
        [Authorize]
        public async Task<PrescriptionViewModel> AddAsync(PrescriptionViewModel PrescriptionViewModel)
        {
            var search = await _PrescriptionService.AddAsync(PrescriptionViewModel);

            return PrescriptionViewModel;
        }

        [HttpPost("Range")]
        [Authorize]
        public async Task<IList<PrescriptionViewModel>> AddRangeAsync(IEnumerable<PrescriptionViewModel> PrescriptionViewModels)
        {
            var query = await _PrescriptionService.AddRangeAsync(PrescriptionViewModels);

            return query;
        }

        [HttpPut]
        [Authorize]
        public async Task<Prescription> UpdateAsync(UpdatePrescriptionViewModel PrescriptionViewModel)
        {

            var query = await _PrescriptionService.UpdateAsync(PrescriptionViewModel);

            return query;
        }

        [HttpPut("Range")]
        [Authorize]
        public async Task<IList<Prescription>> UpdateRangeAsync(IEnumerable<UpdatePrescriptionViewModel> PrescriptionViewModels)
        {
            var query = await _PrescriptionService.UpdateRangeAsync(PrescriptionViewModels);

            return query;
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task DeleteAsync(int id)
        {
            await _PrescriptionService.DeleteAsync(id);
        }

        [HttpGet("Search")]
        [Authorize]
        public async Task<GenericResult<IList<PrescriptionLightViewModel>>> Search(PrescriptionSearchViewModel searchViewModel)
        {
            var query = await _PrescriptionService.Search(searchViewModel);

            return query;
        }

        [HttpGet("lookup/Search")]
        [Authorize]
        public async Task<GenericResult<IList<PrescriptionLookUpViewModel>>> SearchLookUp(PrescriptionLookUpSearchViewModel SearchModel)
        {
            var query = await _PrescriptionService.SearchLookUp(SearchModel);

            return query;
        }

        [HttpGet("View/{id}")]
        [Authorize]
        public async Task<PrescriptionViewViewModel> Search(int id)
        {
            var query = await _PrescriptionService.Search(id);

            return query;
        }

    }
}
