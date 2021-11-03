﻿using Admission.Bussiness.IService;
using Admission.Bussiness.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admission.API.Controllers
{
    [Route("api/universityManagement")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UniversityManagementController : ControllerBase
    {
        private readonly IUniversityManagementService _iUniversityManagementService;
        public UniversityManagementController(IUniversityManagementService iUniversityManagementService)
        {
            _iUniversityManagementService = iUniversityManagementService;
        }

        [HttpGet("getUniversity")]
        public ActionResult GetUniversity([FromQuery] GetById requets)
        {
            var university = _iUniversityManagementService.GetUniversity(requets.Id);

            if (university != null) return StatusCode(200, (new { university }));
            return StatusCode(404, (new { error = "Not found university" }));
        }

        [HttpGet("getUniversities")]
        public ActionResult GetUniversities([FromQuery] SearchUniversity request)
        {
            if (request.Page <= 0 || request.Limit <= 0) return StatusCode(400, (new { error = "Fields '_page', '_limit' cannot be empty or null  AND '_page', '_limit' must be greater than 0" }));
            
            var result = _iUniversityManagementService.GetUniversities(request);
            
            if (result != null) return StatusCode(200, (new
            {
                admins = result["universities"],
                numPage = result["numPage"],
                curentPage = request.Page,
            }));
            return StatusCode(404, (new { error = "Not found any university" }));
        }

        [HttpPost("chuaxong/createUniversity")]
        public async Task<ActionResult> CreateUniversity([FromBody] CreateUniversity request)
        {
            if (await _iUniversityManagementService.CreateUniversity(request)) return StatusCode(201, (new { message = "Create university successed" }));
            return StatusCode(500, (new { error = "Create university failed" }));
        }

        [HttpPut("chuaxong/updateUniversity")]
        public async Task<ActionResult> UpdateUniversity([FromBody] UpdateUniversity request)
        {
            var university = _iUniversityManagementService.GetUniversity(request.Id);

            if (university == null) return StatusCode(404, (new { error = "Not found university" }));
            if (await _iUniversityManagementService.UpdateUniversity(request)) return StatusCode(201, (new { message = "Update university successed" }));
            return StatusCode(500, (new { error = "Update university failed" }));
        }
    }
}
