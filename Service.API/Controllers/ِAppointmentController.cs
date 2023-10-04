using Framework.core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Core.IServices;
using Service.Core.Models.ViewModels.Appointment;
using Service.Entities.entities;

namespace Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _AppointmentService;

        public AppointmentController(IAppointmentService AppointmentService)
        {
            _AppointmentService = AppointmentService;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IQueryable<AppointmentViewModel>>> GetAllAsync()
        {
            var query = await _AppointmentService.GetAllAsync();

            return Ok(query);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AppointmentViewModel>> GetByIdAsync(int id)
        {
            var query = await _AppointmentService.GetByIdAsync(id);

            return Ok(query);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<AppointmentViewModel>> AddAsync(AppointmentViewModel AppointmentViewModel)
        {
            var search = await _AppointmentService.AddAsync(AppointmentViewModel);

            return Created(" ",AppointmentViewModel);
        }

        [HttpPost("Range")]
        [Authorize]
        public async Task<ActionResult<IList<AppointmentViewModel>>> AddRangeAsync(IEnumerable<AppointmentViewModel> AppointmentViewModels)
        {
            var result = await _AppointmentService.AddRangeAsync(AppointmentViewModels);

            return Created(" ",result);
        }


        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Appointment>> UpdateAsync(UpdateAppointMentViewModel AppointmentViewModel)
        {

            var result = await _AppointmentService.UpdateAsync(AppointmentViewModel);

            return Ok(result);
        }

        [HttpPut("Range")]
        [Authorize]
        public async Task<ActionResult<IList<Appointment>>> UpdateRangeAsync(IEnumerable<UpdateAppointMentViewModel> AppointmentViewModels)
        {
            var query = await _AppointmentService.UpdateRangeAsync(AppointmentViewModels);

            return Ok(query);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _AppointmentService.DeleteAsync(id);

            return Ok();
        }


        [HttpGet("Search")]
        [Authorize]
        public async Task<ActionResult<GenericResult<IList<AppointmentLightViewModel>>>> Search(AppointmentSearchViewModel searchModel)
        {
            var query = await _AppointmentService.Search(searchModel);

            return Ok(query);
        }

        [HttpGet("lookup/Search")]
        [Authorize]
        public async Task<ActionResult<GenericResult<IList<AppointmentLookUpViewModel>>>> Search(AppointmentLookUpSearchViewModel searchModel)
        {
            var query = await _AppointmentService.SearchLookUp(searchModel);

            return Ok(query);
        }

        [HttpGet("View/{id}")]
        [Authorize]
        public async Task<ActionResult<AppointmentViewViewModel>> Search(int id)
        {
            var query = await _AppointmentService.Search(id);

            return Ok(query);
        }

    }
}
