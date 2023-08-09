using _00_Basics.Models;
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
        [HttpDelete("{id}", Name="DeleteStudent")]
        public bool DeleteStudent(int id)
        {
            var deleted = AllStudents.Students.FirstOrDefault(i => i.Id == id);
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
    }
}
