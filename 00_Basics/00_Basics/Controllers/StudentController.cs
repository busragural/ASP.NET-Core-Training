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
        public IEnumerable<Student> GetStudents()
        {
            return AllStudents.Students;

        }
        [HttpGet]
        [Route("{id:int}", Name = "GetStudentbyId")]
        public Student GetStudentbyId(int id)
        {
            return AllStudents.Students.FirstOrDefault(i => i.Id == id);
            //return AllStudents.Students.Where(i => i.Id == id).FirstOrDefault();
        }
        [HttpGet("{name:alpha}", Name = "GetStudentbyName")]
        public Student GetStudentbyName(string name)
        {
            return AllStudents.Students.FirstOrDefault(i => i.Name == name);
            //return AllStudents.Students.Where(i => i.Id == id).FirstOrDefault();
        }
        [HttpDelete("{id}", Name="DeleteStudent")]
        public bool DeleteStudent(int id)
        {
            var deleted = AllStudents.Students.FirstOrDefault(i => i.Id == id);
            AllStudents.Students.Remove(deleted);
            return true;
        }

    }
}
