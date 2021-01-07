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
        }

        // POST students
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Student student)
        {
        }

        // PUT students
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Student newStudent,
                                            [FromQuery] string studentName)
        {
        }

        // DELETE students
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] string studentName)
        {
        }

    }
}
