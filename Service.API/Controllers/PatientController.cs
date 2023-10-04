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
        public async Task<ActionResult<IQueryable<PatientViewModel>>> GetAllAsync()
        {
            var query = await _PatientService.GetAllAsync();

            return Ok(query);
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<PatientViewModel>> GetByIdAsync(int id)
        {
            var query = await _PatientService.GetByIdAsync(id);

            return OK(query);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PatientViewModel>> AddAsync(PatientViewModel PatientViewModel)
        {
            var search = await _PatientService.AddAsync(PatientViewModel);

            return Created(" ",PatientViewModel);
        }

        [HttpPost("Range")]
        [Authorize]
        public async Task<ActionResult<IList<PatientViewModel>>> AddRangeAsync(IEnumerable<PatientViewModel> PatientViewModels)
        {
            var result = await _PatientService.AddRangeAsync(PatientViewModels);

            return Created(" ",result);;
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Patient>> UpdateAsync(UpdatePatientViewModel PatientViewModel)
        {

            var result = await _PatientService.UpdateAsync(PatientViewModel);

            return Ok(result);
        }

        [HttpPut("Range")]
        [Authorize]
        public async Task<ActionResult<IList<Patient>>> UpdateRangeAsync(IEnumerable<UpdatePatientViewModel> PatientViewModels)
        {
            var result= await _PatientService.UpdateRangeAsync(PatientViewModels);

            return Ok(result);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _PatientService.DeleteAsync(id);

            return Ok();
        }

        [HttpGet("Search")]
        [Authorize]
        public async Task<ActionResult<GenericResult<IList<PatientLightViewModel>>>> Search(PatientSearchViewModel searchViewModel)
        {
            var query = await _PatientService.Search(searchViewModel);

            return Ok(query);
        }

        [HttpGet("lookup/Search")]
        [Authorize]
        public async Task<ActionResult<GenericResult<IList<PatientLookUpViewModel>>>> SearchLookUp(PatientLookUpSearchViewModel SearchModel)
        {
            var query = await _PatientService.SearchLookUp(SearchModel);

            return Ok(query);
        }

        [HttpGet("View/{id}")]
        [Authorize]
        public async Task<ActionResult<PatientViewViewModel>> Search(int id)
        {
            var query = await _PatientService.Search(id);

            return Ok(query);
        }


    }
}
