using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Backend.Models;

#nullable enable


namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeachersController : ControllerBase
    {

        public TeachersController()
        {
            this.db = new Services.Database();
        }

        private Services.Database db;

        // GET teachers
        [HttpGet]
        public async Task<ActionResult<List<Teacher>>> Get([FromQuery] int facultyId,
                                                           [FromQuery] int count = 10,
                                                           [FromQuery] int offset = 0)
        {
            if (facultyId == null)
                return BadRequest();

            return await this.db.getTeachers(facultyId, count, offset);
        }

        // POST teachers
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Teacher teacher)
        {
            if (teacher == null)
                return BadRequest();

            try
            {
                await this.db.insertTeacher(teacher);
            }
            catch (System.ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        // PUT teachers
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Teacher teacher,
                                            [FromQuery] int id)
        {
            if (teacher == null || id == null)
                return BadRequest();

            try
            {
                await this.db.updateTeacher(id, teacher);
            }
            catch (System.ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        // DELETE teachers
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            if (id == null)
                return BadRequest();

            await this.db.removeTeacher(id);

            return Ok();
        }

    }
}

