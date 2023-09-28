using Framework.core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Core.IServices;
using Service.Core.Models.ViewModels.Patient;
using Service.Entities.entities;

namespace Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _PatientService;

        public PatientController(IPatientService PatientService)
        {
            _PatientService = PatientService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IQueryable<PatientViewModel>> GetAllAsync()
        {
            var query = await _PatientService.GetAllAsync();

            return query;
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<PatientViewModel> GetByIdAsync(int id)
        {
            var query = await _PatientService.GetByIdAsync(id);

            return query;
        }

        [HttpPost]
        [Authorize]
        public async Task<PatientViewModel> AddAsync(PatientViewModel PatientViewModel)
        {
            var search = await _PatientService.AddAsync(PatientViewModel);

            return PatientViewModel;
        }

        [HttpPost("Range")]
        [Authorize]
        public async Task<IList<PatientViewModel>> AddRangeAsync(IEnumerable<PatientViewModel> PatientViewModels)
        {
            var query = await _PatientService.AddRangeAsync(PatientViewModels);

            return query;
        }


        [HttpPut]
        [Authorize]
        public async Task<Patient> UpdateAsync(UpdatePatientViewModel PatientViewModel)
        {

            var query = await _PatientService.UpdateAsync(PatientViewModel);

            return query;
        }

        [HttpPut("Range")]
        [Authorize]
        public async Task<IList<Patient>> UpdateRangeAsync(IEnumerable<UpdatePatientViewModel> PatientViewModels)
        {
            var query= await _PatientService.UpdateRangeAsync(PatientViewModels);

            return query;
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task DeleteAsync(int id)
        {
            await _PatientService.DeleteAsync(id);
        }

        [HttpGet("Search")]
        [Authorize]
        public async Task<GenericResult<IList<PatientLightViewModel>>> Search(PatientSearchViewModel searchViewModel)
        {
            var query = await _PatientService.Search(searchViewModel);

            return query;
        }

        [HttpGet("lookup/Search")]
        [Authorize]
        public async Task<GenericResult<IList<PatientLookUpViewModel>>> SearchLookUp(PatientLookUpSearchViewModel SearchModel)
        {
            var query = await _PatientService.SearchLookUp(SearchModel);

            return query;
        }

        [HttpGet("View/{id}")]
        [Authorize]
        public async Task<PatientViewViewModel> Search(int id)
        {
            var query = await _PatientService.Search(id);

            return query;
        }


    }
}
