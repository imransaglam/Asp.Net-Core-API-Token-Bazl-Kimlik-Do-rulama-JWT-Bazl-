using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Dtos;

namespace AuthServer.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        //DI Containerlar için bir base class tanımlıyoruz
        public IActionResult ActionResultInstance<T>(Response<T> response)where T : class
        {
            return new ObjectResult(response)//ObjectResult Ok(),BadRequest() gibi classların bir üst classıdır.
            {
                StatusCode = response.StatusCode,
            };
        }
    }
}
