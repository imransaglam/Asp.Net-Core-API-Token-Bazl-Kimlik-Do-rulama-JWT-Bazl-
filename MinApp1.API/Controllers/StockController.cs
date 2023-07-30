using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MinApp1.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        [HttpGet]   
        public IActionResult GetStock() 
        {
            var userName = HttpContext.User.Identity.Name;
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            //veri tabanında userId veya userName üzerinden gerekli dataları çekebilirzi
            return Ok($"Stock İşlemleri =>UserName:{ userName}-userId:{ userIdClaim.Value}");
        }
    }
}
