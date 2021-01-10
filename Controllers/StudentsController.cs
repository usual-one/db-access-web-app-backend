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
        public async Task<ActionResult<List<Models.Student>>> Get([FromQuery] string groupName,
                                                                  [FromQuery] int count = 10,
                                                                  [FromQuery] int offset = 0)
        {
            return await this.db.getStudents(groupName, count, offset);
        }

        // POST students
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Models.Student student)
        {
            await this.db.insertStudent(student);

            return Ok();
        }

        // PUT students
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Models.Student newStudent,
                                            [FromQuery] string studentName)
        {
            await this.db.updateStudent(studentName, newStudent);
            
            return Ok();
        }

        // DELETE students
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] string studentName)
        {
            await this.db.removeStudent(studentName);

            return Ok();
        }

    }
}
