using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Backend.Models;


namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeachersController : ControllerBase
    {

        // GET teachers
        [HttpGet]
        public async Task<ActionResult<List<Teachers>>> Get([FromQuery] string? facultyName = "")
        {
        }

        // POST teachers
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Teacher teacher)
        {
        }

        // PUT teachers
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Teacher newTeacher,
                                            [FromQuery] string teacherName)
        {
        }

        // DELETE teachers
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] string teacherName)
        {
        }

    }
}

