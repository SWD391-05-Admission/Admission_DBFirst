using Admission.Bussiness.Request;
using Admission.Bussiness.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admission.API.Controllers
{
    [Route("api/v1/uniImage")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UniImageController : ControllerBase
    {
        private readonly IUniImageService _iUniImageService;
        public UniImageController(IUniImageService iUniImageService)
        {
            _iUniImageService = iUniImageService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUniAddress([FromBody] CreateUniImage request)
        {
            if (await _iUniImageService.CreateUniImage(request)) return StatusCode(201, (new { message = "Create uniImage successed" }));
            return StatusCode(500, (new { message = "Create uniImage failed" }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUniAddress([FromBody] UpdateUniImage request)
        {
            var uniImage = _iUniImageService.GetUniImage(request.Id);

            if (uniImage != null) return StatusCode(400, (new { message = "UniImage already exists" }));
            if ((bool)request.IsLogo)
            {
                if (_iUniImageService.GetUniImageLogo()) return StatusCode(400, (new { message = "Have orther image is logo" }));
            }
            if (await _iUniImageService.UpdateUniImage(request)) return StatusCode(201, (new { message = "Update uniImage successed" }));
            return StatusCode(500, (new { message = "Update uniImage failed" }));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUniAddress([FromBody] GetById request)
        {
            var uniImage = _iUniImageService.GetUniImage(request.Id);

            if (uniImage == null) return StatusCode(400, (new { message = "UniImage does not exists" }));
            if (await _iUniImageService.DeleteUniImage(uniImage)) return StatusCode(200, (new { message = "Delete uniImage successed" }));
            return StatusCode(500, (new { message = "Delete uniImage failed" }));
        }
    }
}
