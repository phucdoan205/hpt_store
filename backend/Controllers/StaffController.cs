using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/staff")]
    [Authorize(Roles = "Staff")]
    public class StaffController : ControllerBase
    {
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            return Ok(new
            {
                message = "Welcome Staff",
                time = DateTime.UtcNow
            });
        }

        [HttpGet("orders")]
        public IActionResult Orders()
        {
            return Ok("Staff can process orders here");
        }

        [HttpGet("products")]
        public IActionResult Products()
        {
            return Ok("Staff can manage products");
        }

        [HttpGet("posts")]
        public IActionResult Posts()
        {
            return Ok("Staff can manage blog posts");
        }
    }
}
