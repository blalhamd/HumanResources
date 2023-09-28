using Framework.core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Core.IServices;
using Service.Core.Models.ViewModels.Doctor;
using Service.Entities.entities;

namespace Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _DoctorService;

        public DoctorController(IDoctorService DoctorService)
        {
            _DoctorService = DoctorService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IQueryable<DoctorViewModel>> GetAllAsync()
        {
            var query = await _DoctorService.GetAllAsync();

            return query;
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<DoctorViewModel> GetByIdAsync(int id)
        {
            var query = await _DoctorService.GetByIdAsync(id);

            return query;
        }


        [HttpPost]
        [Authorize]
        public async Task<DoctorViewModel> AddAsync(DoctorViewModel DoctorViewModel)
        {
            var search = await _DoctorService.AddAsync(DoctorViewModel);

            return DoctorViewModel;
        }

        [HttpPost("Range")]
        public async Task<IList<DoctorViewModel>> AddRangeAsync(IEnumerable<DoctorViewModel> DoctorViewModels)
        {
            var query = await _DoctorService.AddRangeAsync(DoctorViewModels);

            return query;
        }

        [HttpPut]
        [Authorize]
        public async Task<Doctor> UpdateAsync(UpdateDoctorViewModel DoctorViewModel)
        {

            var query= await _DoctorService.UpdateAsync(DoctorViewModel);

            return query;
        }

        [HttpPut("Range")]
        [Authorize]
        public async Task<IList<Doctor>> UpdateRangeAsync(IEnumerable<UpdateDoctorViewModel> DoctorViewModels)
        {
            var query = await _DoctorService.UpdateRangeAsync(DoctorViewModels);

            return query;
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task DeleteAsync(int id)
        {
            await _DoctorService.DeleteAsync(id);
        }

        [HttpGet("Search")]
        [Authorize]
        public async Task<GenericResult<IList<DoctorLightViewModel>>> Search(DoctorSearchViewModel searchViewModel)
        {
            var query = await _DoctorService.Search(searchViewModel);

            return query;
        }

        [HttpGet("lookup/Search")]
        [Authorize]
        public async Task<GenericResult<IList<DoctorLookUpViewModel>>> SearchLookUp(DoctorLookUpSearchViewModel SearchModel)
        {
            var query = await _DoctorService.SearchLookUp(SearchModel);

            return query;
        }

        [HttpGet("View/{id}")]
        [Authorize]
        public async Task<DoctorViewViewModel> Search(int id)
        {
            var query = await _DoctorService.Search(id);

            return query;
        }

    }
}
