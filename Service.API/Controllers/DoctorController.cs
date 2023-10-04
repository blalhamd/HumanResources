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
        public async Task<ActionResult<IQueryable<DoctorViewModel>>> GetAllAsync()
        {
            var query = await _DoctorService.GetAllAsync();

            return Ok(query);
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<DoctorViewModel>> GetByIdAsync(int id)
        {
            var query = await _DoctorService.GetByIdAsync(id);

            return Ok(query);
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<DoctorViewModel>> AddAsync(DoctorViewModel DoctorViewModel)
        {
            var search = await _DoctorService.AddAsync(DoctorViewModel);

            return Created(" ",DoctorViewModel);
        }

        [HttpPost("Range")]
        public async Task<ActionResult<IList<DoctorViewModel>>> AddRangeAsync(IEnumerable<DoctorViewModel> DoctorViewModels)
        {
            var query = await _DoctorService.AddRangeAsync(DoctorViewModels);

            return Created(" ",query);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Doctor>> UpdateAsync(UpdateDoctorViewModel DoctorViewModel)
        {

            var query= await _DoctorService.UpdateAsync(DoctorViewModel);

            return Ok(query);
        }

        [HttpPut("Range")]
        [Authorize]
        public async Task<ActionResult<IList<Doctor>>> UpdateRangeAsync(IEnumerable<UpdateDoctorViewModel> DoctorViewModels)
        {
            var query = await _DoctorService.UpdateRangeAsync(DoctorViewModels);

            return Ok(query);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _DoctorService.DeleteAsync(id);

            return ok()
        }

        [HttpGet("Search")]
        [Authorize]
        public async Task<ActionResult<GenericResult<IList<DoctorLightViewModel>>>> Search(DoctorSearchViewModel searchViewModel)
        {
            var query = await _DoctorService.Search(searchViewModel);

            return Ok(query);
        }

        [HttpGet("lookup/Search")]
        [Authorize]
        public async Task<ActionResult<GenericResult<IList<DoctorLookUpViewModel>>>> SearchLookUp(DoctorLookUpSearchViewModel SearchModel)
        {
            var query = await _DoctorService.SearchLookUp(SearchModel);

            return Ok(query);
        }

        [HttpGet("View/{id}")]
        [Authorize]
        public async Task<ActionResult<DoctorViewViewModel>> Search(int id)
        {
            var query = await _DoctorService.Search(id);

            return Ok(query);
        }

    }
}
