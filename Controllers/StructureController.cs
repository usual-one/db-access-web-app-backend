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

        // POST structure/faculties
        [HttpPost("faculties")]
        public async Task<ActionResult> Faculties([FromBody] Models.Faculty faculty)
        {
            if (faculty == null)
                return BadRequest();
            
            await this.db.insertFaculty(faculty);

            return Ok();
        }

        // PUT structure/faculties
        [HttpPut("faculties")]
        public async Task<ActionResult> Faculties([FromBody] Models.Faculty faculty,
                                            [FromQuery] int id)
        {
            if (faculty == null || id == null)
                return BadRequest();

            await this.db.updateFaculty(id, faculty);
            
            return Ok();
        }

        // DELETE structure/faculties
        [HttpDelete("faculties")]
        public async Task<ActionResult> Faculties([FromQuery] int id)
        {
            if (id == null)
                return BadRequest();
            
            await this.db.removeFaculty(id);

            return Ok();
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

        // POST structure/groups
        [HttpPost("groups")]
        public async Task<ActionResult> Groups([FromBody] Models.Group group)
        {
            if (group == null)
                return BadRequest();
            
            try
            {
                await this.db.insertGroup(group);
            }
            catch (System.ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        // PUT structure/groups
        [HttpPut("groups")]
        public async Task<ActionResult> Groups([FromBody] Models.Group group,
                                               [FromQuery] int id)
        {
            if (group == null || id == null)
                return BadRequest();

            try
            {
                await this.db.updateGroup(id, group);
            }
            catch (System.ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        // DELETE structure/groups
        [HttpDelete("groups")]
        public async Task<ActionResult> Groups([FromQuery] int id)
        {
            if (id == null)
                return BadRequest();
            
            await this.db.removeGroup(id);

            return Ok();
        }

    }
}
