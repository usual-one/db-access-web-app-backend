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
        public async Task<ActionResult<List<Teacher>>> Get([FromQuery] string facultyName,
                                                           [FromQuery] int count,
                                                           [FromQuery] int offset )
        {
            return await this.db.getTeachers(facultyName, count, offset);
        }

        // POST teachers
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Teacher teacher)
        {
            await this.db.insertTeacher(teacher);

            return Ok();
        }

        // PUT teachers
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Teacher newTeacher,
                                            [FromQuery] string teacherName)
        {
            await this.db.updateTeacher(teacherName, newTeacher);

            return Ok();
        }

        // DELETE teachers
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] string teacherName)
        {
            await this.db.removeTeacher(teacherName);

            return Ok();
        }

    }
}

