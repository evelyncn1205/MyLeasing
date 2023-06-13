using Microsoft.AspNetCore.Mvc;
using MyLeasing.Web.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyLeasing.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LesseesController : ControllerBase
    {
        private readonly ILesseeRepository _lesseeRepository;

        public LesseesController(ILesseeRepository lesseeRepository)
        {
            _lesseeRepository=lesseeRepository;
        }

        [HttpGet]
        public IActionResult GetLessee()
        {
            return Ok(_lesseeRepository.GetAllWithUsers());
        }

    }
}
