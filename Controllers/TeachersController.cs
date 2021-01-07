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

        // GET teachers
        [HttpGet]
        public async Task<ActionResult<List<Teacher>>> Get([FromQuery] string? facultyName = "")
        {
            return new List<Teacher>();
        }

        // POST teachers
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Teacher teacher)
        {
            return Ok();
        }

        // PUT teachers
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Teacher newTeacher,
                                            [FromQuery] string teacherName)
        {
            return Ok();
        }

        // DELETE teachers
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] string teacherName)
        {
            return Ok();
        }

    }
}

