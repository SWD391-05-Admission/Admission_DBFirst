using Admission.Bussiness.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Admission.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Student")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _iWalletService;
        public WalletController(IWalletService iWalletService)
        {
            _iWalletService = iWalletService;
        }

        [HttpGet("wallet")]
        public ActionResult GetWallet()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            int userId = Convert.ToInt32(claim[0].Value);

            var wallet = _iWalletService.GetWallet(userId);

            if (wallet == null) return StatusCode(404, (new { message = "Not found wallet" }));
            return StatusCode(200, (new { wallet }));
        }
    }
}
