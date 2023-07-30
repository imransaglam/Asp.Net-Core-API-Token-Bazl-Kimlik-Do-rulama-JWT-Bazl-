using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MinApp1.API.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {

        [Authorize(Roles = "admin",Policy = "AnkaraPolicy")]
        [Authorize(Policy = "AgePolicy")]
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
