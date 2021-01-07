using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Backend.Models;


namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {

        // GET students
        [HttpGet]
        public async Task<ActionResult<List<Student>>> Get([FromQuery] string groupName)
        {
            return new List<Student>();
        }

        // POST students
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Student student)
        {
            return Ok();
        }

        // PUT students
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Student newStudent,
                                            [FromQuery] string studentName)
        {
            return Ok();
        }

        // DELETE students
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] string studentName)
        {
            return Ok();
        }

    }
}
