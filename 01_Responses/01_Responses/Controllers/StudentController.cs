using _00_Basics.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Xml.Linq;

namespace _00_Basics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All", Name = "GetStudents")]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return Ok(AllStudents.Students);     //Ok - 200 - Success 
        }
        [HttpGet]
        [Route("{id:int}", Name = "GetStudentbyId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<Student> GetStudentbyId(int id)
        {

            if(id <= 0)
            {
                return BadRequest(); //BadRequest - 400 - Client Error
            }

            var student = AllStudents.Students.FirstOrDefault(i => i.Id == id);
            if (student == null){
                return NotFound($"The student with id {id} not found");     //NotFound - 404 - Client Error
                //return NotFound()
            }
            

            return Ok(student);      
           
        }
        [HttpGet("{name:alpha}", Name = "GetStudentbyName")]
        public ActionResult<Student> GetStudentbyName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest(); //BadRequest - 400 - Client Error
            }

            return Ok(AllStudents.Students.FirstOrDefault(i => i.Name == name));
           
        }
        [HttpDelete("Delete/{id}", Name="DeleteStudent")]
        public bool DeleteStudent(int id)
        {
            var deleted = AllStudents.Students.FirstOrDefault(i => i.Id == id);
            if(deleted == null)
            {
                return false;
            }
            AllStudents.Students.Remove(deleted);
            return true;
        }
        [HttpPost]
        [Route("Create")]
        public ActionResult<Student> CreateStudent([FromBody]Student model)
        {
            if(model == null)
            {
                return BadRequest();
            }
            int newId = AllStudents.Students.LastOrDefault().Id + 1;
            Student student = new Student
            {
                Id = newId,
                Name = model.Name,
                Address = model.Address,
                Email = model.Email,

            };
            AllStudents.Students.Add(student);
            model.Id = student.Id;
            //status - 201
            return CreatedAtRoute("GetStudentbyId", new { id = model.Id }, model);  //prepare the url based on the data 
           
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult UpdateStudent([FromBody]Student model)
        {
            if(model == null || model.Id <= 0)
            {
                BadRequest();
            }
            var existing = AllStudents.Students.FirstOrDefault(i => i.Id == model.Id);
            if(existing == null)
            {
                return NotFound();
            }
            existing.Name = model.Name;
            existing.Address = model.Address;
            existing.Email = model.Email;
            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}/UpdatePartial")]
        public ActionResult UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<Student> patchDocument)  //install newtonsoft json nuget and add this line to support patch
        {
            if (patchDocument == null || id <= 0)
            {
                BadRequest();
            }
            var existing = AllStudents.Students.FirstOrDefault(i => i.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            var tmpStudent = new Student
            {
                Id = existing.Id,
                Name = existing.Name,
                Address = existing.Address,
                Email = existing.Email,
            };

            patchDocument.ApplyTo(tmpStudent, ModelState);  //if there is a error, modelstate shows us
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            existing.Name = tmpStudent.Name;
            existing.Address = tmpStudent.Address;
            existing.Email = tmpStudent.Email;
            return NoContent();
        }
    }
}
