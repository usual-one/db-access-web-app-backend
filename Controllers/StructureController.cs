using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Backend.Models;


namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StructureController : ControllerBase
    {

        // GET structure/faculties
        [HttpGet("faculties")]
        public async Task<ActionResult<List<Faculty>>> Faculties([FromQuery] int? count = 10,
                                                                 [FromQuery] int? offset = 0)
        {
        }

        // GET structure/groups
        [HttpGet("groups")]
        public async Task<ActionResult<List<Group>>> Faculties([FromQuery] string? facultyname = "",
                                                               [FromQuery] int? count = 10,
                                                               [FromQuery] int? offset = 0)
        {
        }

    }
}
