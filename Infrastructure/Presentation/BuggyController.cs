using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController:ControllerBase
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound();//404
        }

        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            throw new Exception();
            return Ok();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest();//400
        }

        [HttpGet("badrequest/{id}")]
        public IActionResult GetBadRequest(int id)
        {
            return BadRequest(id);
        }

        [HttpGet("unauthorized")]
        public IActionResult GetUnAuthorized()
        {
            return Unauthorized();//401
        }

    }
}
