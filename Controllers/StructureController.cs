using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public StructureController()
        {
            this.db = new Services.Database();
        }

        private Services.Database db;

        // GET structure/faculties
        [HttpGet("faculties")]
        public async Task<ActionResult<List<Faculty>>> Faculties([FromQuery] int count = 10,
                                                                 [FromQuery] int offset = 0)
        {
            return await this.db.getFaculties(count, offset);
        }

        // GET structure/groups
        [HttpGet("groups")]
        public async Task<ActionResult<List<Group>>> Groups([FromQuery] int facultyId,
                                                            [FromQuery] int count = 10,
                                                            [FromQuery] int offset = 0)
        {
            if (facultyId == null)
               return BadRequest(); 

            return await this.db.getGroups(facultyId, count, offset);
        }

    }
}
