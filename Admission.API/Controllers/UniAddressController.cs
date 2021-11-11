using Admission.Bussiness.Request;
using Admission.Bussiness.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Admission.API.Controllers
{
    [Route("api/v1/uniAddress")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UniAddressController : ControllerBase
    {
        private readonly IUniAddressService _iUniAddressService;
        public UniAddressController(IUniAddressService iUniAddressService)
        {
            _iUniAddressService = iUniAddressService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUniAddress([FromBody] CreateUniAddress request)
        {
            if (await _iUniAddressService.CreateUniAddress(request)) return StatusCode(201, (new { message = "Create uniMajor successed" }));
            return StatusCode(500, (new { message = "Create uniMajor failed" }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUniAddress([FromBody] UpdateUniAddress request)
        {
            var uniMajor = _iUniAddressService.GetUniAddress(request.Id);

            if (uniMajor != null) return StatusCode(400, (new { message = "UniAddress already exists" }));
            if (await _iUniAddressService.UpdateUniAddress(request)) return StatusCode(201, (new { message = "Create uniAddress successed" }));
            return StatusCode(500, (new { message = "Create uniAddress failed" }));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUniAddress([FromBody] GetById request)
        {
            var uniAddress = _iUniAddressService.GetUniAddress(request.Id);

            if (uniAddress == null) return StatusCode(400, (new { message = "UniAddress does not exists" }));
            if (await _iUniAddressService.DeleteUniAddress(uniAddress)) return StatusCode(200, (new { message = "Delete uniAddress successed" }));
            return StatusCode(500, (new { message = "Delete uniAddress failed" }));
        }
    }
}
