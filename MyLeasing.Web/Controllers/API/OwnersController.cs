using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLeasing.Web.Data;

namespace MyLeasing.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepository;
        public OwnersController(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        [HttpGet]
        public IActionResult GetOwner()
        {
            return Ok(_ownerRepository.GetAllWithUsers());
        }
    }
}
