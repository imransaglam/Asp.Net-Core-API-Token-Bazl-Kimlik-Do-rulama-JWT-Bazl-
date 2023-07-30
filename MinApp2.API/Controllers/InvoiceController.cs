using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MinApp2.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        [HttpGet]   
        public IActionResult GetInvoice()
        {
            var userName = HttpContext.User.Identity.Name;
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            //veri tabanında userId veya userName üzerinden gerekli dataları çekebilirzi
            return Ok($"Invoice İşlemleri => UserName:{userName}-userId:{userIdClaim.Value}");
        }
    }
}
