using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            return NotFound();
        }

        [HttpGet("servererror")]
        public IActionResult GetServerErrorRequest()
        {
            throw new Exception();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest(int id)
        {
            return BadRequest();
        }

        [HttpGet("badrequest/{id}")]
        public IActionResult GetValidationBadRequest(int id)
        {
            return BadRequest();
        }

        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorizedRequest()
        {
            return Unauthorized();
        }
    }
}
