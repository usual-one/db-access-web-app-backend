using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {

        public StudentsController()
        {
            this.db = new Services.Database();
        }

        private Services.Database db;

        // GET students
        [HttpGet]
        public async Task<ActionResult<List<Models.Student>>> Get([FromQuery] int groupId,
                                                                  [FromQuery] int count = 10,
                                                                  [FromQuery] int offset = 0)
        {
            if (groupId == null)
                return BadRequest();

            return await this.db.getStudents(groupId, count, offset);
        }

        // POST students
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Models.Student student)
        {
            if (student == null)
                return BadRequest();

            await this.db.insertStudent(student);

            return Ok();
        }

        // PUT students
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Models.Student student,
                                            [FromQuery] int id)
        {
            if (student == null || id == null)
                return BadRequest();

            await this.db.updateStudent(id, student);
            
            return Ok();
        }

        // DELETE students
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            if (id == null)
                return BadRequest();
            
            await this.db.removeStudent(id);

            return Ok();
        }

    }
}
