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
        public async Task<IQueryable<AppointmentViewModel>> GetAllAsync()
        {
            var query = await _AppointmentService.GetAllAsync();

            return query;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<AppointmentViewModel> GetByIdAsync(int id)
        {
            var query = await _AppointmentService.GetByIdAsync(id);

            return query;
        }

        [HttpPost]
        [Authorize]
        public async Task<AppointmentViewModel> AddAsync(AppointmentViewModel AppointmentViewModel)
        {
            var search = await _AppointmentService.AddAsync(AppointmentViewModel);

            return AppointmentViewModel;
        }

        [HttpPost("Range")]
        [Authorize]
        public async Task<IList<AppointmentViewModel>> AddRangeAsync(IEnumerable<AppointmentViewModel> AppointmentViewModels)
        {
            var query = await _AppointmentService.AddRangeAsync(AppointmentViewModels);

            return query;
        }


        [HttpPut]
        [Authorize]
        public async Task<Appointment> UpdateAsync(UpdateAppointMentViewModel AppointmentViewModel)
        {

            var query = await _AppointmentService.UpdateAsync(AppointmentViewModel);

            return query;
        }

        [HttpPut("Range")]
        [Authorize]
        public async Task<IList<Appointment>> UpdateRangeAsync(IEnumerable<UpdateAppointMentViewModel> AppointmentViewModels)
        {
            var query = await _AppointmentService.UpdateRangeAsync(AppointmentViewModels);

            return query;
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task DeleteAsync(int id)
        {
            await _AppointmentService.DeleteAsync(id);
        }


        [HttpGet("Search")]
        [Authorize]
        public async Task<GenericResult<IList<AppointmentLightViewModel>>> Search(AppointmentSearchViewModel searchModel)
        {
            var query = await _AppointmentService.Search(searchModel);

            return query;
        }

        [HttpGet("lookup/Search")]
        [Authorize]
        public async Task<GenericResult<IList<AppointmentLookUpViewModel>>> Search(AppointmentLookUpSearchViewModel searchModel)
        {
            var query = await _AppointmentService.SearchLookUp(searchModel);

            return query;
        }

        [HttpGet("View/{id}")]
        [Authorize]
        public async Task<AppointmentViewViewModel> Search(int id)
        {
            var query = await _AppointmentService.Search(id);

            return query;
        }

    }
}
